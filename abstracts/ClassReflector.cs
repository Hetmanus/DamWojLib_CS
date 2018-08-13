//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: Creation of mechanizm that will simplify referencing other classes by use of reflections
//
// Legend:
// 'Reflector' => class that inherits from ClassReflector
// 'Target' => class that is suppose to be referenced by this 'Reflector'
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DamWojLib
{
    /// <summary>
    /// Use to reference other class from current assembly using reflections
    /// </summary>
    public abstract class ClassReflector
    {
        static readonly Dictionary<Type, Type> s_reflectedTypes = new Dictionary<Type, Type>();
        static readonly Dictionary<Type, Dictionary<MethodInfo, MethodInfo>> s_reflectedTypeMembers = new Dictionary<Type, Dictionary<MethodInfo, MethodInfo>>();
        static readonly Dictionary<Type, Exception> s_notReflectedTypes = new Dictionary<Type, Exception>();

        static ClassReflector()
        {
            #region finding all types that are suppose to be to reflected
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            //finding all 'Reflector'
            IEnumerable<Type> classReflectors = allTypes.Where(
                    type => typeof(ClassReflector) != type &&
                    typeof(ClassReflector).IsAssignableFrom(type) &&
                    type.DeclaringType == null);
            foreach (var type in classReflectors)
            {
                //detecting if current 'Reflector' has custom className to reflect if not present its own name is used
                FieldInfo classNameConst = type.GetField("className", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
                if (classNameConst != null && (!classNameConst.IsLiteral || classNameConst.IsInitOnly)) { classNameConst = null; }
                //detecting if current 'Reflector' has custom nameSpace to reflect if not present no no namespace is used
                FieldInfo nameSpaceConst = type.GetField("nameSpace", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
                if (nameSpaceConst != null && (!nameSpaceConst.IsLiteral || nameSpaceConst.IsInitOnly)) { nameSpaceConst = null; }

                try
                {
                    //finding 'Target'
                    Type reflectedType = Methods.FindType(
                        nameSpaceConst == null ? null : (string)nameSpaceConst.GetValue(null) + '.',
                        classNameConst == null ? type.Name : (string)classNameConst.GetValue(null));

                    //detecting if this 'Reflector' reflects back to itself
                    if (reflectedType == type)
                    {
                        throw new Exception(string.Format("Reflector \"{0}\" is reflecting itself", type));
                    }

                    //members present inside 'Reflector' that have to be present on 'Target' side
                    IEnumerable<MethodInfo> membersToReflect = type.GetMethods(
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                        .Where(memb => memb.DeclaringType != typeof(ClassReflector));
                    //all members present in 'Target'
                    Dictionary<string, MethodInfo> reflectedMembers = reflectedType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToDictionary(memb => memb.ToString());
                    //Dictionary to hold all 'Reflector' members that found their 'Target' counter part.
                    Dictionary<MethodInfo, MethodInfo> membersDict = new Dictionary<MethodInfo, MethodInfo>();

                    #region finidng all members that are suppose to be present on 'Target' side
                    foreach (var toReflect in membersToReflect)
                    {
                        MethodInfo fitinMember;
                        if (reflectedMembers.TryGetValue(toReflect.ToString(), out fitinMember))
                        {
                            reflectedMembers.Remove(toReflect.ToString());
                            membersDict.Add(toReflect, fitinMember);
                        }
                        else
                        {
                            throw new MissingMemberException(reflectedType.Name, toReflect.ToString());
                        }
                    }
                    #endregion

                    s_reflectedTypeMembers.Add(type, membersDict);
                    s_reflectedTypes.Add(type, reflectedType);
                }
                catch (Exception e)
                {
                    s_notReflectedTypes.Add(type, e);
                }
            }
            #endregion

            // if error occured throwing them out
            if (s_notReflectedTypes.HasValue())
            {
                throw new ReflectionTypeLoadException(s_notReflectedTypes.Keys.ToArray(), s_notReflectedTypes.Values.ToArray());
            }
        }

        public static Type GetReflectedType<T>() where T : ClassReflector
        {
            return s_reflectedTypes[typeof(T)];
        }
        public static bool IsLoaded<T>() where T : ClassReflector
        {
            return s_notReflectedTypes.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Call this method from ClassReflector inheriting class static methods to execute its counterpart on referenced object
        /// </summary>
        protected static object Static_Invoke(params object[] parameters)
        {
            MethodBase method = new StackFrame(1, true).GetMethod();
            try
            {
                if (method.IsStatic || method.IsConstructor)
                {
                    MethodBase toInvoke =
                        s_reflectedTypeMembers
                        [method.DeclaringType.DeclaringType ?? method.DeclaringType]
                        [method as MethodInfo];
                    return toInvoke.Invoke(null, parameters);
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MissingMemberException(method.DeclaringType.ToString(), method.Name);
            }
            throw new NotImplementedException();
        }

        readonly ClassReflector m_reflectedInstance;

        public ClassReflector()
        {
            throw new NotImplementedException();
        }
        public ClassReflector(ClassReflector instance)
        {
            m_reflectedInstance = instance;
        }

        /// <summary>
        /// Call this method from ClassReflector inheriting class methods to execute its counterpart on referenced object
        /// </summary>
        protected object Instance_Invoke(params object[] parameters)
        {
            MethodBase method = new StackFrame(1, true).GetMethod();
            try
            {
                if (method.IsStatic || method.IsConstructor)
                {
                    MethodBase toInvoke =
                        s_reflectedTypeMembers
                        [method.DeclaringType.DeclaringType ?? method.DeclaringType]
                        [method as MethodInfo];
                    return toInvoke.Invoke(null, parameters);
                }
                else
                {
                    MethodBase toInvoke =
                        s_reflectedTypeMembers[method.DeclaringType.DeclaringType ?? method.DeclaringType]
                        [method as MethodInfo];
                    return toInvoke.Invoke(m_reflectedInstance, parameters);
                }
            }
            catch(KeyNotFoundException)
            {
                throw new MissingMemberException(method.DeclaringType.ToString(), method.Name);
            }
        }
    }
}

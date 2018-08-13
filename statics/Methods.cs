//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: General static methods
//
//----------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DamWojLib
{
    public static class Methods
    {
        /// <summary>
        /// Gets type using class assembly qualified names defined in Consts.assemblyQulifiedFormats
        /// </summary>
        /// <param name="nameSpace">null for no namespace</param>
        public static Type FindType(string nameSpace, string className)
        {
            if (!className.HasValue()) throw new ArgumentNullException("className", "Can not be null, or empty");
            if (nameSpace.HasValue()) nameSpace += '.';
            Type type = Consts.assemblyQulifiedFormats
                        .Select(assQualForm => Type.GetType(string.Format(assQualForm, nameSpace ?? string.Empty, className ?? string.Empty)))
                        .SingleOrDefault(reflTyp => reflTyp != null);
            if(type == null)
            {
                throw new NullReferenceException(string.Format("Could not find type {0}{1}", nameSpace ?? string.Empty, className ?? string.Empty));
            }
            else
            {
                return type;
            }
        }

        public static T DeepClone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (System.Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static MemoryStream BinaryFormatterSerialize<T>(T data)
        {
            var answer = new MemoryStream();
            var formater = new BinaryFormatter();
            formater.Serialize(answer, data);
            return answer;
        }
        public static T BinaryFormatterDeserialize<T>(MemoryStream data)
        {
            object answer = new BinaryFormatter().Deserialize(data);
            return (T)answer;
        }
    }
}
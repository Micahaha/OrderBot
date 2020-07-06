using System;
using System.ComponentModel;
using System.Xml;

namespace BooruAPI.Core.Utilities
{
    /// <summary> A helper class used for XML related functions.</summary>
    public static class XmlUtility
    {
        /// <summary> Reads the attribute value as a string.</summary>
        /// <param name="reader"> The reader of the XML data.</param>
        /// <param name="attributeName"> The name of the attribute to read.</param>
        public static string ReadAttributeValue(XmlReader reader, string attributeName)
        {
            reader.MoveToAttribute(attributeName);
            if (reader.ReadAttributeValue())
                return reader.Value;
            else
                throw new Exception($"Cannot read value from {attributeName}");
        }

        /// <summary> Reads the attribute value as a nullable <typeparamref name="T"/>.</summary>
        /// <param name="reader"> The reader of the XML data.</param>
        /// <param name="attributeName"> The name of the attribute to read.</param>
        public static T? ReadAributeValueAsNullableStruct<T>(XmlReader reader, string attributeName) where T : struct
        {
            reader.MoveToAttribute(attributeName);
            if (reader.ReadAttributeValue())
            {
                if (string.IsNullOrEmpty(reader.Value))
                    return null;
                else if (TryConvertToStruct<T>(reader.Value, out var value))
                    return value;
                else
                    throw new Exception($"Cannot read {reader.Value} as {typeof(T).Name}");
            }
            else
                return null;
        }

        /// <summary> Reads the attribute value as a <typeparamref name="T"/>.</summary>
        /// <param name="reader"> The reader of the XML data.</param>
        /// <param name="attributeName"> The name of the attribute to read.</param>
        public static T ReadAtributeValueAsStruct<T>(XmlReader reader, string attributeName) where T : struct
        {
            reader.MoveToAttribute(attributeName);
            if (reader.ReadAttributeValue())
            {
                if (TryConvertToStruct<T>(reader.Value, out var value))
                    return value;
                else
                    return default;
            }
            else
                throw new Exception($"Cannot read value from {attributeName}");
        }

        /// <summary> Tries to convert a string to a <typeparamref name="T"/>.</summary>
        /// <typeparam name="T"> The struct the string converts to.</typeparam>
        /// <param name="input"> The value string to convert.</param>
        /// <param name="result"> The resulting <typeparamref name="T"/> value.</param>
        /// <returns> True if a conversion is possible.</returns>
        public static bool TryConvertToStruct<T>(string input, out T result) where T : struct
        {
            result = default;

            if (string.IsNullOrEmpty(input))
                return false;

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                return false;
            if (!converter.CanConvertFrom(typeof(string)))
                return false;

            result = (T)converter.ConvertFromString(input);

            return true;
        }
    }
}

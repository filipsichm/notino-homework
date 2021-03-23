using Converter.Enums;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Converter
{
    public class XmlConverter : IConverter<XDocument>, ISerializationConverter
    {
        public Format Format => Format.Xml;

        public XDocument Parse(string input)
        {
            return XDocument.Parse(input);
        }

        public T Convert<T>(XDocument xDoc)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            T result;

            using (var reader = xDoc.Root.CreateReader())
                result = (T)xmlSerializer.Deserialize(reader);

            return result;
        }

        public XDocument Convert<T>(T input)
        {
            var xmlSerializer = new XmlSerializer(input.GetType());

            var xDoc = new XDocument();
            using (var writer = xDoc.CreateWriter())
                xmlSerializer.Serialize(writer, input);

            return xDoc;
        }

        public T DeserializeObject<T>(string input)
        {
            var xDoc = XDocument.Parse(input);
            return Convert<T>(xDoc);
        }

        public string SerializeObject<T>(T model)
        {
            var xDoc = Convert(model);

            if (xDoc.Declaration == null)
                xDoc.Declaration = new XDeclaration("1.0", "utf-8", null);

            return $"{xDoc.Declaration}{Environment.NewLine}{xDoc}";
        }
    }
}

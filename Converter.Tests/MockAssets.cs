using Converter.Models;
using System.Xml.Linq;

namespace Converter.Tests
{
    public static class MockAssets
    {
        public static Document SeedDocumentTestDocument()
        {
            return new Document() { Title = "Test", Text = "This is a test document!" };
        }

        public static XDocument SeedXDocumentDocument(Document model)
        {
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            return
                 new XDocument(
                   new XElement("Document",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XElement("Title", $"{model.Title}"),
                    new XElement("Text", $"{model.Text}")
                   )
                 );
        }

        public static string SeedJsonDocument(Document model)
        {
            return $@"{{'Title':'{model.Title}','Text':'{model.Text}'}}".Replace("'", "\"");
        }

        public static string SeedXmlDocument(Document model)
        {
            return (
                @"<?xml version='1.0' encoding='utf-8'?>
                <Document xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                  <Title>" + model.Title + @"</Title>
                  <Text>" + model.Text + @"</Text>
                </Document>")
                .Replace("'", "\"");
        }
    }
}

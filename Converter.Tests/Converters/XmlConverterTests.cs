using Converter.Models;
using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit;

namespace Converter.Tests.Converters
{
    public class XmlConverterTests
    {
        private readonly XmlConverter _sut;

        public XmlConverterTests()
        {
            _sut = new XmlConverter();
        }

        [Fact]
        public void Convert_FromDocumentModelToXDocument()
        {
            var model = MockAssets.SeedDocumentTestDocument();
            var expectedXDoc = MockAssets.SeedXDocumentDocument(model);

            var actualXDocument = _sut.Convert(model);

            actualXDocument.Should().BeEquivalentTo(expectedXDoc);
        }

        [Fact]
        public void Convert_FromXDocumentToDocumentModel()
        {
            var expectedModel = MockAssets.SeedDocumentTestDocument();
            var xDoc = MockAssets.SeedXDocumentDocument(expectedModel);

            var actualModel = _sut.Convert<Document>(xDoc);

            actualModel.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public void DeserializeObject_ResultModelIsDeserializedAccordingToFormat()
        {
            var expectedModel = MockAssets.SeedDocumentTestDocument();

            var input = MockAssets.SeedXmlDocument(expectedModel);

            var actualModel = _sut.DeserializeObject<Document>(input);

            actualModel.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public void SerializeObject_ResultModelIsSerializedAccordingToFormat()
        {
            var model = MockAssets.SeedDocumentTestDocument();
            var expectedText = MockAssets.SeedXmlDocument(model);

            var actualText = _sut.SerializeObject(model);

            var actualTextNormalized = Regex.Replace(actualText, @"\s+", string.Empty);
            var expectedTextNormalized = Regex.Replace(expectedText, @"\s+", string.Empty);

            actualTextNormalized.Should().BeEquivalentTo(expectedTextNormalized);
        }
    }
}

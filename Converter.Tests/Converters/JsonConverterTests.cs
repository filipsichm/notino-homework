using Converter.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Converter.Tests.Converters
{
    // NOTE: These aren't very useful when using JsonConvert...
    public class JsonConverterTests
    {
        private readonly JsonConverter _sut;

        public JsonConverterTests()
        {
            _sut = new JsonConverter();
        }

        [Fact]
        public void Parse_ResultThrowExceptionIfStringIsNotInJsonFormat()
        {
            Assert.Throws<JsonReaderException>(() => _sut.Parse("test"));
        }

        [Fact]
        public void Convert_FromDocumentModelToJsont()
        {
            var model = MockAssets.SeedDocumentTestDocument();

            var expectedJson = MockAssets.SeedJsonDocument(model);

            var actualJson = _sut.Convert(model);

            actualJson.Should().BeEquivalentTo(expectedJson);
        }

        [Fact]
        public void Convert_FromJsonToDocumentModel()
        {
            var expectedModel = MockAssets.SeedDocumentTestDocument();
            var json = MockAssets.SeedJsonDocument(expectedModel);

            var actualModel = _sut.Convert<Document>(json);

            actualModel.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public void DeserializeObject_ResultModelIsDeserializedAccordingToFormat()
        {
            var expectedModel = MockAssets.SeedDocumentTestDocument();

            var input = MockAssets.SeedJsonDocument(expectedModel);

            var actualModel = _sut.DeserializeObject<Document>(input);

            actualModel.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public void SerializeObject_ResultModelIsSerializedAccordingToFormat()
        {
            var model = MockAssets.SeedDocumentTestDocument();
            var expectedText = MockAssets.SeedJsonDocument(model);

            var actualText = _sut.SerializeObject(model);

            actualText.Should().BeEquivalentTo(expectedText);
        }
    }
}

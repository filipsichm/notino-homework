using Converter.Converters;
using Converter.Enums;
using FluentAssertions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace Converter.Tests
{
    public class ConversionHandlerTests
    {
        private readonly ConversionHandler _sut;

        public ConversionHandlerTests()
        {
            _sut = new ConversionHandler(new List<ISerializationConverter> { new XmlConverter(), new JsonConverter() });
        }

        [Fact]
        public void ConvertFromXmlToJsonFormat_ResultOutputInJsonFormat()
        {
            var model = MockAssets.SeedDocumentTestDocument();
            var xml = MockAssets.SeedXmlDocument(model);

            var expected = MockAssets.SeedJsonDocument(model);

            var actual = _sut.Convert(Format.Xml, Format.Json, xml);

            var actualTextNormalized = Regex.Replace(actual, @"\s+", string.Empty);
            var expectedTextNormalized = Regex.Replace(expected, @"\s+", string.Empty);

            actualTextNormalized.Should().BeEquivalentTo(expectedTextNormalized);
        }

        [Fact]
        public void ConvertFromJsonToXmlFormat_ResultOutputInXmlFormat()
        {
            var model = MockAssets.SeedDocumentTestDocument();
            var json = MockAssets.SeedJsonDocument(model);

            var expected = MockAssets.SeedXmlDocument(model);

            var actual = _sut.Convert(Format.Json, Format.Xml, json);

            var actualTextNormalized = Regex.Replace(actual, @"\s+", string.Empty);
            var expectedTextNormalized = Regex.Replace(expected, @"\s+", string.Empty);

            actualTextNormalized.Should().BeEquivalentTo(expectedTextNormalized);
        }
    }
}

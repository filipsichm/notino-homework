using Converter.Enums;
using Converter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Converter.Converters
{
    public interface IConversionHandler
    {
        string Convert(Format source, Format target, string input);
        ISerializationConverter GetConverter(Format format);
    }

    public class ConversionHandler : IConversionHandler
    {
        private readonly IEnumerable<ISerializationConverter> _converters;

        public ConversionHandler(IEnumerable<ISerializationConverter> converters)
        {
            _converters = converters;
        }

        public string Convert(Format source, Format target, string input)
        {
            var sourceConverter = GetConverter(source);
            var targetConverter = GetConverter(target);

            var model = sourceConverter.DeserializeObject<Document>(input);

            return targetConverter.SerializeObject(model);
        }

        public ISerializationConverter GetConverter(Format format) => _converters.Where(x => x.Format == format).Single();
    }
}

using Converter.Enums;
using Converter.StreamHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Converter
{
    public class JsonConverter : IConverter<string>, ISerializationConverter
    {
        public Format Format => Format.Json;

        public string Parse(string input)
        {
            try
            {
                JToken.Parse(input);
                return input;
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException("Input is not valid JSON!");
            }
        }

        public string Convert<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public T Convert<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public T DeserializeObject<T>(string input)
        {
            var json = Parse(input);
            return Convert<T>(json);
        }

        public string SerializeObject<T>(T model)
        {
            return Convert(model);
        }
    }
}

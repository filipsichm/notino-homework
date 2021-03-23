using Converter.Enums;

namespace Converter
{
    public interface IConverter<T>
    {
        T Parse(string input);
        TResult Convert<TResult>(T input);
        T Convert<TInput>(TInput input);
    }

    public interface ISerializationConverter
    {
        Format Format { get; }
        T DeserializeObject<T>(string text);
        string SerializeObject<T>(T model);
    }
}

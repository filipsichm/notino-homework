using Converter.Enums;
using System.Threading.Tasks;

namespace Converter.StreamHandlers
{
    public interface IStreamService
    {
        Source Stream { get; }
        Task<string> ReadAsync(string path);
        Task WriteAsync(string path, string text);
    }
}

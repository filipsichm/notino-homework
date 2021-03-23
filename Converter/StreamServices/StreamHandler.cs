using Converter.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converter.StreamHandlers
{
    public interface IStreamHandler
    {
        Task<string> ReadAsync(Source source, string path);
        Task WriteAsync(Source source, string path, string text);
        IStreamService GetStreamService(Source source);
    }

    public class StreamHandler : IStreamHandler
    {
        private readonly IEnumerable<IStreamService> _streamServices;
        public StreamHandler(IEnumerable<IStreamService> streamServices)
        {
            _streamServices = streamServices;
        }

        public async Task<string> ReadAsync(Source source, string path)
        {
            return await GetStreamService(source).ReadAsync(path);
        }

        public async Task WriteAsync(Source source, string path, string text)
        {
            await GetStreamService(source).WriteAsync(path, text);
        }

        public IStreamService GetStreamService(Source source) => _streamServices.Where(x => x.Stream == source).Single();
    }
}

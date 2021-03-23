using Converter.Enums;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Converter.StreamHandlers
{
    public class HttpStreamService : IStreamService
    {
        private readonly HttpClient _httpClient;

        public Source Stream => Source.Http;

        public HttpStreamService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> ReadAsync(string path)
        {
            return await _httpClient.GetStringAsync(path);
        }

        public Task WriteAsync(string path, string text)
        {
            throw new Exception("HttpStreamService - WriteAsync() - This action is not supported!");
        }
    }
}

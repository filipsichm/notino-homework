using Converter.Enums;
using Google.Cloud.Storage.V1;
using System.IO;
using System.Threading.Tasks;

namespace Converter.StreamHandlers
{
    //NOTE: draft
    public class CloudStorageStreamService //: IStreamService
    {
        public Source Stream => Source.CloudStorage;

        private readonly StorageClient _storage;
        private readonly FileStreamService _fileStreamService;

        public CloudStorageStreamService(FileStreamService fileStreamService)
        {
            _storage = StorageClient.Create();
            _fileStreamService = fileStreamService;
        }

        public async Task<string> ReadAsync(string path)
        {
            using var outputFile = File.OpenWrite("temp-path");
            // TODO: "bucket-name", "object-name" - get from 'path'
            await _storage.DownloadObjectAsync("bucket-name", "object-name", outputFile);

            return await _fileStreamService.ReadAsync("temp-path");
        }

        public async Task WriteAsync(string path, string text)
        {
            await _fileStreamService.WriteAsync("temp-path", text);

            using var fileStream = File.OpenRead("temp-path");
            await _storage.UploadObjectAsync("bucket-name", "object-name", "text/plain", fileStream);
        }
    }
}

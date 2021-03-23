using Converter.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Converter.StreamHandlers
{
    public class FileStreamService : IStreamService
    {
        public Source Stream => Source.FileSystem;

        public async Task WriteAsync(string path, string text)
        {
            try
            {
                var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
                using var outputFile = new StreamWriter(stream);
                await outputFile.WriteAsync(text);
            } 
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ReadAsync(string path)
        {
            try
            {
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: 4096, useAsync: true);
                var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

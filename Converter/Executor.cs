using Converter.Configuration;
using Converter.Converters;
using Converter.StreamHandlers;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Converter
{
    public class Executor
    {
        private readonly IConversionHandler _conversionHandler;
        private readonly IStreamHandler _streamHandler;
        private readonly AppConfiguration _options;

        public Executor(IConversionHandler conversionHandler, IStreamHandler streamHandler, IOptions<AppConfiguration> options)
        {
            _conversionHandler = conversionHandler;
            _options = options.Value;
            _streamHandler = streamHandler;
        }

        public async Task Run()
        {
            // TODO: Validate AppConfiguration (_options.Validate())

            Console.WriteLine($"{_options.SourceFile.Format}->{_options.TargetFile.Format} - format conversion has started");

            var input = await _streamHandler.ReadAsync(_options.SourceFile.Source, _options.SourceFile.Path);

            var output = _conversionHandler.Convert(_options.SourceFile.Format, _options.TargetFile.Format, input);

            await _streamHandler.WriteAsync(_options.TargetFile.Source, _options.TargetFile.Path, output);

            Console.WriteLine($"Format conversion completed successfully...");
            Console.ReadKey(true);
        }
    }
}

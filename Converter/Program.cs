using Converter;
using Converter.Configuration;
using Converter.Converters;
using Converter.StreamHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Notino.Homework
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.Configure<AppConfiguration>(configuration);
            services.AddScoped<Executor>();
            services.AddScoped<IStreamHandler, StreamHandler>();
            services.AddScoped<IConversionHandler, ConversionHandler>();
            services.AddScoped<FileStreamService>();

            Assembly.GetAssembly(typeof(ISerializationConverter))
                .DefinedTypes
                .Where(x => typeof(ISerializationConverter).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .ToList()
                .ForEach(
                    x => services.AddScoped(typeof(ISerializationConverter), x)
                );

            Assembly.GetAssembly(typeof(IStreamService))
                .DefinedTypes
                .Where(x => typeof(IStreamService).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .ToList()
                .ForEach(
                    x => services.AddScoped(typeof(IStreamService), x)
                );

            var serviceProvider = services.BuildServiceProvider();

            var executor = serviceProvider.GetService<Executor>();

            await executor.Run();
        }
    }
}
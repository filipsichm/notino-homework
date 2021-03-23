using Converter.Enums;

namespace Converter.Configuration
{
    public class AppConfiguration
    {
        public File SourceFile { get; set; }
        public File TargetFile { get; set; }
    }

    public class File
    {
        public Source Source { get; set; }
        public string Path { get; set; }
        public Format Format { get; set; }
    }
}

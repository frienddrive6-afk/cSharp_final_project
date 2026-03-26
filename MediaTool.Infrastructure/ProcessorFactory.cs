using MediaTool.Core;

namespace MediaTool.Infrastructure;

public static class ProcessorFactory
{
    public static IMetadataProcessor Create(string path)
    {
        string ext = Path.GetExtension(path).ToLower();
        return ext switch 
        {
            ".jpg" or ".jpeg" => new JpegProcessor(),
            ".mp4" => new Mp4Processor(),
            _ => throw new Exception($"Формат {ext} не поддерживается")
        };
    }
}
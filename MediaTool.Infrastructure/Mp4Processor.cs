namespace MediaTool.Infrastructure;

using FFMpegCore;

using MediaTool.Core;


public class Mp4Processor : IMetadataProcessor
{
    public MetadataContainer Read(string filePath)
    {
        var analysis = FFProbe.Analyse(filePath);

        analysis.Format.Tags.TryGetValue("artist", out string? artist);
        analysis.Format.Tags.TryGetValue("author", out string? author);

        var container = new MetadataContainer
        {
            Common = new CommonMetadata
            {
                FileName = Path.GetFileName(filePath),
                CreationDate = File.GetCreationTime(filePath),
                Author = artist ?? author ?? "Unknown",
                Size = new FileInfo(filePath).Length,
            },

            Video = new VideoMetadata
            {
                Codec = analysis.PrimaryVideoStream?.CodecName ?? "Unknown",
                BitRate = analysis.PrimaryVideoStream?.BitRate,
                FrameRate = (double?)Math.Round(analysis.PrimaryVideoStream?.FrameRate ?? 0, 2),
            }
        
        };

        return container;

        // return new MetadataContainer { Common = new CommonMetadata() };
    }

    public void Write(string filePath, MetadataContainer data)
    {
        
    }


}
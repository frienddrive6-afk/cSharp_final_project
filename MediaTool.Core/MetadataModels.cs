namespace MediaTool.Core;

public class CommonMetadata
{
    public string? FileName { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? Author { get; set; }
    public long Size { get; set; }
}

public class PhotoMetadata
{
    public string? Model { get; set; }
    public string? ExposureTime { get; set; }
    public string? FNumber { get; set; }
    public int? Iso { get; set; }

}

public class VideoMetadata
{
    public string? Codec { get; set; }
    public long? BitRate { get; set; }
    public double? FrameRate { get; set; }

}
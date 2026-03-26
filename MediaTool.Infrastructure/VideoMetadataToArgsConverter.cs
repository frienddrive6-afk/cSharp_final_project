namespace MediaTool.Infrastructure;

using MediaTool.Core;

public static class VideoMetadataToArgsConverter
{
    
    private static readonly Dictionary<string, string> TagMap = new()
    {
        {nameof(CommonMetadata.Author), "artist"},
        {nameof(CommonMetadata.FileName), "title"},
        {nameof(CommonMetadata.CreationDate), "creation_time"},
        // {nameof(CommonMetadata.Size), "filesize"},
        // {nameof(VideoMetadata.Codec), "codec"},
        // {nameof(VideoMetadata.BitRate), "bitrate"},
        // {nameof(VideoMetadata.FrameRate), "framerate"},
    };


    public static List<string> GetFfmpegArgs(MetadataContainer data)
    {
        List<string> args = new List<string>();

        if(data.Common != null)
        {
            
            foreach(var prop in data.Common.GetType().GetProperties())
            {
                var value = prop.GetValue(data.Common);

                if (value != null && TagMap.TryGetValue(prop.Name, out var tag))
                {
                    string valueStr = value is DateTime date ? date.ToString("yyyy-MM-ddTHH:mm:ss") : value.ToString()!;
                    args.AddRange(new[] { "-metadata", $"{tag}=\"{valueStr}\"" });
                }
            }

        }

        // if(data.Video != null)
        // {
        //     foreach(var prop in data.Video.GetType().GetProperties())
        //     {
        //         var value = prop.GetValue(data.Video);

        //         if(value != null && TagMap.TryGetValue(prop.Name, out var tag))
        //         {
        //             args.Add($"-{tag}=\"{value}\"");
        //         }
        //     }
            
        // }


        return args;

    }


}
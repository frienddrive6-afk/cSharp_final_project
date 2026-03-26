namespace MediaTool.Infrastructure;

using System.Reflection;
using MediaTool.Core;


public static class MetadataToArgsConverter
{
    
    private static readonly Dictionary<string, string> TagMap = new()
    {
        { nameof(CommonMetadata.Author), "Artist" },
        { nameof(CommonMetadata.CreationDate), "DateTimeOriginal" },
        { nameof(PhotoMetadata.Model), "Model" },
        { nameof(PhotoMetadata.ExposureTime), "ExposureTime" },
        { nameof(PhotoMetadata.FNumber), "FNumber" },
        { nameof(PhotoMetadata.Iso), "ISO" },

    };


    public static List<string> GetExifToolArgs(MetadataContainer data)
    {
        List<string> args = new List<string>();

        AddArgs(data.Common, args);
        AddArgs(data.Photo, args);

        return args;

    }


    private static void AddArgs(object? obj, List<string> args)
    {
        if(obj == null) return;


        foreach(var prop in obj.GetType().GetProperties())
        {
            var value = prop.GetValue(obj);

            if(value != null && TagMap.TryGetValue(prop.Name, out var tag))
            {
                args.Add($"-{tag}=\"{value}\"");
            }
        }

    }

}
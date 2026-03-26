namespace MediaTool.Infrastructure;

using FFMpegCore;

using MediaTool.Core;


public class Mp4Processor : IMetadataProcessor
{
    public MetadataContainer Read(string filePath)
    {
        var analysis = FFProbe.Analyse(filePath);

        Dictionary<string, string>? tags = analysis.Format.Tags;

        string? dateStr = tags.GetValueOrDefault("creation_time") ?? tags.GetValueOrDefault("date");
        DateTime? creationDate = null;

        if (DateTime.TryParse(dateStr, out var parsedDate))
        {
            creationDate = parsedDate;
        }
        else
        {
            creationDate = File.GetCreationTime(filePath);
        }

        var container = new MetadataContainer
        {
            Common = new CommonMetadata
            {
                FileName = Path.GetFileName(filePath),
                CreationDate = creationDate,
                Author = tags.GetValueOrDefault("artist") ?? tags.GetValueOrDefault("author") ?? "Unknown",
                Size = new FileInfo(filePath).Length
            },
            Video = new VideoMetadata
            {
                Codec = analysis.PrimaryVideoStream?.CodecName ?? "Unknown",
                BitRate = analysis.PrimaryVideoStream?.BitRate,
                FrameRate = (double?)Math.Round(analysis.PrimaryVideoStream?.FrameRate ?? 0, 2)
            }
        };

        return container;
    }

    public void Write(string filePath, MetadataContainer data)
    {
#if DEBUG
    Console.WriteLine($"[DEBUG] {nameof(Mp4Processor)}.{nameof(Write)}: Начинаю запись для {filePath}");
#endif

        string tempOutput = filePath + ".tmp.mp4";
        
        List<string> args = new List<string> {"-i",$"\"{filePath}\""};

        args.AddRange(VideoMetadataToArgsConverter.GetFfmpegArgs(data));

        args.AddRange(new[] { "-c", "copy", $"\"{tempOutput}\"" });

        string argString = string.Join(" ",args);

#if DEBUG
    Console.WriteLine($"[DEBUG] Выполняю команду: ffmpeg {argString}");
#endif

        CommandResult result = CommandRunner.Execute("ffmpeg", argString);

        if(result.IsSuccess)
        {
            File.Delete(filePath);
            File.Move(tempOutput, filePath);
#if DEBUG
        Console.WriteLine($"[DEBUG] Запись успешна для {filePath}");
#endif
        }else
        {
            if(File.Exists(tempOutput))
            {
                File.Delete(tempOutput);
            }
            throw new Exception($"Ошибка FFMpeg: {result.StandardError}");
        }
        
    }


}
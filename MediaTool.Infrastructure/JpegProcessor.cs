namespace MediaTool.Infrastructure;

using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MediaTool.Core;


public class JpegProcessor : IMetadataProcessor
{
    public MetadataContainer Read(string filePath)
    {
        var directories = ImageMetadataReader.ReadMetadata(filePath);
        var subIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        var ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

        var container = new MetadataContainer { Common = new CommonMetadata(), Photo = new PhotoMetadata() };

        container.Common.FileName = Path.GetFileName(filePath);
        
        if (ifd0 != null)
        {
            container.Common.Author = ifd0.GetString(ExifIfd0Directory.TagArtist);

            if (ifd0.TryGetDateTime(ExifIfd0Directory.TagDateTime, out var date))
            {
                container.Common.CreationDate = date;
            }
        }

        container.Common.Size = new FileInfo(filePath).Length;

        if (subIfd != null)
        {
            if (subIfd.TryGetRational(ExifSubIfdDirectory.TagExposureTime, out var exp))
            {
                container.Photo.ExposureTime = exp.ToString();
            }

            if (subIfd.TryGetInt32(ExifSubIfdDirectory.TagIsoEquivalent, out var iso))
            {
                container.Photo.Iso = iso;
            }
        }

        if (ifd0 != null)
        {
            container.Photo.Model = ifd0.GetDescription(ExifIfd0Directory.TagModel);

            if (ifd0.TryGetRational(ExifIfd0Directory.TagFNumber, out var fNum))
            {
                container.Photo.FNumber = fNum.ToString();
            }
        }

        return container;
    }






    public void Write(string filePath, MetadataContainer data)
    {
        
        var argList = MetadataToArgsConverter.GetExifToolArgs(data);
    
        if(argList.Count == 0) return;


        string args = string.Join(" ", argList) + $" \"{filePath}\"";

        Console.WriteLine($"[DEBUG] Выполняю команду: exiftool {args}");

        var result = CommandRunner.Execute("exiftool", args);

        if(result.IsSuccess == false)
        {
            throw new Exception($"Ошибка ExifTool: {result.StandardError}");
        }

        Console.WriteLine($"Результат ExifTool: {result.StandardOutput}");

    }


}
namespace MediaTool.Core;

public static class MetadataFormatter
{
    public static void Print(MetadataContainer container)
    {
        if (container.Common != null)
        {
            Console.WriteLine($"--- Общие: {container.Common.FileName} ---");
            Console.WriteLine($"Автор: {container.Common.Author ?? "Неизвестен"}");
            Console.WriteLine($"Время создания: {container.Common.CreationDate}");
            Console.WriteLine($"Размер: {container.Common.Size / 1024} KB");
        }
        
        if (container.Photo != null)
        {
            Console.WriteLine($"--- Фото ---");
            Console.WriteLine($"Модель: {container.Photo.Model}");
            System.Console.WriteLine($"Время контакта: {container.Photo.ExposureTime}");
            System.Console.WriteLine($"Диафрагма {container.Photo.FNumber}");
            System.Console.WriteLine($"ISO: {container.Photo.Iso}");
        }

        if(container.Video != null)
        {
            System.Console.WriteLine("--- Видео ---");
            System.Console.WriteLine($"Кодек: {container.Video.Codec}");
            System.Console.WriteLine($"Битрейт: {container.Video.BitRate}");
            System.Console.WriteLine($"Частота кадров: {container.Video.FrameRate}");
        }
        System.Console.WriteLine();
    }
}

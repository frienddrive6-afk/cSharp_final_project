namespace MediaTool.Core;

public class FileScanner
{
    public IEnumerable<string> GetFiles(string folderPath, ISpecification<string> filter)
    {
        string[] allFiles = Directory.GetFiles(folderPath);

        foreach(string file in allFiles)
        {
            if(filter.IsSatisfied(file)) 
            {
                yield return file;
            }
        }
    }


}

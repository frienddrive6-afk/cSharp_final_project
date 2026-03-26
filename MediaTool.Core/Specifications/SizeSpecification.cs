namespace MediaTool.Core.Specifications;

public class SizeSpecification : ISpecification<string>
{
    private readonly long _min;
    private readonly long _max;

    public SizeSpecification(long min, long max)
    {
        _min = min;
        _max = max;
    }

    public bool IsSatisfied(string fullPath)
    {
        long length = new FileInfo(fullPath).Length;
        return length >= _min && length <= _max;
    }

}
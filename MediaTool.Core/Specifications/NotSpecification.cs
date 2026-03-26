namespace MediaTool.Core.Specifications;

public class NotSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _spec;
    public NotSpecification(ISpecification<T> spec) => _spec = spec;

    public bool IsSatisfied(T item)
    {
        bool result = _spec.IsSatisfied(item);
        return !result;
    }
}
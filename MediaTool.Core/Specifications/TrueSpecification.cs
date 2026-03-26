namespace MediaTool.Core.Specifications;

public class TrueSpecification<T> : ISpecification<T>
{
    public bool IsSatisfied(T item) => true;
}
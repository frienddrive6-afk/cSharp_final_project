namespace MediaTool.Core.Specifications;

public class FalseSpecification<T> : ISpecification<T>
{
    public bool IsSatisfied(T item) => false;
}
namespace MediaTool.Core.Specifications;

public class OrSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;
    public OrSpecification(ISpecification<T> left, ISpecification<T> right) 
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfied(T item) => _left.IsSatisfied(item) || _right.IsSatisfied(item);
    
}
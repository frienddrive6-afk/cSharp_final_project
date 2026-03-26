namespace MediaTool.Core;



public interface ISpecification<T>
{
    
   public bool IsSatisfied(T item); 
}
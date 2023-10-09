using Core.Specification;
using System.Linq.Expressions;

namespace Core;

public class Specification<T> : ISpecification<T>
{
    public Specification()
    {

    }
    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } 
        = new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression) {
        Includes.Add(includeExpression); 
    }
    protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)
    {
        OrderByDescending = OrderByDescendingExpression;
    }
    protected void ApplyPaging(int skip, int take)
    {
        Take = take;
        Skip = skip;
        IsPagingEnabled = true;
    }
}

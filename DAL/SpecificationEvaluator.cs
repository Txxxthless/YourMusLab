using DAL.Interface;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SpecificationEvaluator<T>
        where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(
            IQueryable<T> inputQuery,
            ISpecification<T> specification
        )
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            query = specification.Includes.Aggregate(
                query,
                (current, include) => current.Include(include)
            );

            return query;
        }
    }
}

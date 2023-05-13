using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// build a ginaric or lets say a Specification query with flixable paramters and list of includes 
        /// </summary>
        /// <param name="_dbContext">your dbContext With the table set or set of data type</param>
        /// <param name="spec">your specs (list of includes and the Criteria)</param>
        /// <returns>a full query with the send data</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> _dbContext,ISpecification<TEntity> spec)
        {
            var query = _dbContext;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (currentQ, nextInclude) => currentQ.Include(nextInclude));

            return query;
        }
    }
}

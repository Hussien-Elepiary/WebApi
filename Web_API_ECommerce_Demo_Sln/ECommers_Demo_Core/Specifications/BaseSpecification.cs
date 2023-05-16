using ECommerce_Demo_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> sortAsc { get; set; }
        public Expression<Func<T, object>> sortDesc { get; set; }

        /// <summary>
        /// will build a query with a criteria and make a list of includs for specified data
        /// </summary>
        /// <param name="criteria"> where condition criteria </param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// put a list of includes to build your query with a specified data
        /// </summary>
        public BaseSpecification()
        {
            
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            sortAsc = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderBy)
        {
            sortDesc = orderBy;
        }
    }
}

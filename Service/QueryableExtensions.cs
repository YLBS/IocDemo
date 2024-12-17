using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 扩展查询添加
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 扩展
        /// </summary>
        public static IQueryable<TEntity> WhereIf<TEntity>(
            this IQueryable<TEntity> queryable,
            bool condition,
            Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (condition)
            {
                return queryable.Where(predicate);
            }

            return queryable;
        }

        /// <summary>
        /// 接受更多的条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(
            this IQueryable<TEntity> queryable,
            IDictionary<Expression<Func<TEntity, bool>>, bool> conditions) where TEntity : class
        {
            foreach (var condition in conditions)
            {
                queryable = queryable.WhereIf(condition.Value, condition.Key);
            }

            return queryable;
        }
        /*
         * var conditions = new Dictionary<Expression<Func<User, bool>>, bool>
         * { { u => u.Age >= 18, true },
         *   { u => u.Name.StartsWith("A"), someCondition }
         * };
         */
    }
}

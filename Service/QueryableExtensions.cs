using System.Linq.Expressions;

namespace StudentManagementSystem.Service
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }
            return source;
        }

        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            bool condition,
            Func<T, bool> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }
            return source;
        }
    }
}


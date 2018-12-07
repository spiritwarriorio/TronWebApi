
namespace TRON.WebApi.Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    public static class Extension
    {
        public static string EstandarGuionFecha(this DateTime d) => (d == DateTime.MinValue
                                                         ? "-"
                                                         : d.ToString(CultureInfo.CurrentCulture));

        public static void SelectItemsList(this string s, Action<int> fnSeleccionar, char sep = '|')
        {
            if (!string.IsNullOrEmpty(s))
            {
                var lst = Array.ConvertAll(s.Split(sep), int.Parse).ToList();
                if (lst.IsNotNull()) { foreach (var item in lst) { fnSeleccionar(item); } }
            }
        }

        public static List<int> SelectItemsList(this string s, char sep = '|')
        {
            if (!string.IsNullOrEmpty(s))
            {
                var lst = Array.ConvertAll(s.Split(sep), int.Parse).ToList();
                if (lst.IsNotNull()) { return lst; }
            }

            return null;
        }

        public static bool IsNotNull<T>(this List<T> lstValid) => (lstValid != default(List<T>) && lstValid.Count > 0);

        public static bool IsNotNull<T>(this Collection<T> lstValid) => (lstValid != default(Collection<T>) && lstValid.Count > 0);

        public static bool IsNotNull<T>(this ICollection<T> lstValid) => (lstValid != null && lstValid.Count > 0);

        public static bool IsNotNull<T>(this T valid) where T : class => (valid != default(T));

        public static bool IsValid(this string s) => (s.IsNotNull() && s.Trim().Length > 0);

        public static bool IsNotNull<T>(this T[] valid) => (valid != default(T[]) && valid.Length > 0);

        public enum Order
        {
            Asc,
            Desc
        }

        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            Order direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == Order.Asc ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }
    }
}
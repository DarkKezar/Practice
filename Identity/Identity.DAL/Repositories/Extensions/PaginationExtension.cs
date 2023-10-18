using System.Collections;

namespace Identity.DAL.Repositories.Extensions;

public static class PaginationExtension
{
    public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int page, int count)
    {
        if(count < 0 || page < 0)
        {
            throw new Exception("Invalid data");
        }

        return query.Skip(count * (page - 1)).Take(count);
    }
}

using Academy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Extensions
{
    public static class QueryExtensions
    {
        public async static Task<PagedList<T>> ToPagedList<T>(
            this IQueryable<T> source, 
            int pageSize, 
            int pageNumber,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await source.CountAsync(cancellationToken);

            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}

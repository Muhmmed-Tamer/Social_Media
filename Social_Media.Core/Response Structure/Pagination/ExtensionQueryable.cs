using Microsoft.EntityFrameworkCore;

namespace Social_Media.Core.Response_Structure.Pagination
{
    public static class ExtensionQueryable
    {

        public static async Task<Paginated<T>> ToPaginatedAsync<T>(this IQueryable<T> Items, int PageNumber, int PageSize)
        {
            try
            {
                PageNumber = PageNumber <= 0 ? 1 : PageNumber;
                PageSize = PageSize <= 0 ? 10 : PageSize;
                int Count = await Items.CountAsync();
                Items = Items.Skip((PageNumber - 1) * PageSize).Take(PageSize);
                return new Paginated<T>()
                {
                    TotalPages = (int)Math.Ceiling((decimal)Count / PageSize),
                    PageSize = PageSize,
                    CurrentPage = PageNumber,
                    TotalCount = Count,
                    Values = await Items.ToListAsync()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

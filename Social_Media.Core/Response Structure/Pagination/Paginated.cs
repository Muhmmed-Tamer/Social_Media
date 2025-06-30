namespace Social_Media.Core.Response_Structure.Pagination
{
    public class Paginated<T>
    {
        public List<T> Values { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public Paginated() { }
    }
}

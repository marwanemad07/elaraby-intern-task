namespace OnlineShopping.Infrastructure.Specifications
{
    public class PageSpecificationParameters
    {
        public PageSpecificationParameters(string? sort, string? search, int? pageSize, int? pageNumber)
        {
            Sort = sort;
            Search = search;
            PageSize = pageSize ?? PageSize;
            PageNumber = pageNumber ?? PageNumber;
        }

        private const int MaxPageSize = 30;
        private int _pageSize = 10;
        private string? _search;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(MaxPageSize, value);
        }

        public int PageNumber { get; set; } = 1;
        public string? Sort { get; set; }
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
    }
}

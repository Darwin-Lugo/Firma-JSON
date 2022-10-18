namespace Firma.Core.QueryFilters
{
    public class Filter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? StatusData { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

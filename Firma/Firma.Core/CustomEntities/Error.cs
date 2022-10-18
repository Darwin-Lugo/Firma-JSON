namespace Firma.Core.CustomEntities
{
    public class Error
    {
        public int Code { get; set; }
        public string Title { get; set; } = null!;
        public string? Detail { get; set; } = null!;
    }
}

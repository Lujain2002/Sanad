namespace Sanad.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LongDescription { get; set; }

        public string? ImageUrl { get; set; }
        public string? Thumbnails { get; set; }
        public string? Tags { get; set; }
        public string? BuyLink { get; set; }
        public string? DetailsLink { get; set; }
        public string? DemoLink { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? ProductYearId { get; set; }
        public ProductYear? ProductYear { get; set; }
    }
}

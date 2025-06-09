namespace Sanad.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LongDescription { get; set; }

        public string? ImageUrl { get; set; }
        public int Year { get; set; }
        public string Category { get; set; } = string.Empty;


        public string? Thumbnails { get; set; } // JSON أو comma-separated
        public string? Tags { get; set; }        // "AI,Marketing,SEO"
        public string? BuyLink { get; set; }
        public string? DetailsLink { get; set; }
        public string? DemoLink { get; set; }
    }
}

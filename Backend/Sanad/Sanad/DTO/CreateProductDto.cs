using Microsoft.AspNetCore.Mvc;

namespace Sanad.DTO
{
    public class CreateProductDto
    {
        [FromForm(Name = "title")]
        public string? Title { get; set; }

        [FromForm(Name = "description")]
        public string? Description { get; set; }

        [FromForm(Name = "longdescription")]
        public string? LongDescription { get; set; }

        [FromForm(Name = "imageurl")]
        public IFormFile? ImageUrl { get; set; }

        [FromForm(Name = "thumbnails")]
        public List<IFormFile>? Thumbnails { get; set; }

        [FromForm(Name = "year")]
        public int? Year { get; set; }

        [FromForm(Name = "category")]
        public string? Category { get; set; } = string.Empty;

        [FromForm(Name = "tags")]
        public string? Tags { get; set; }

        [FromForm(Name = "buylink")]
        public string? BuyLink { get; set; }

        [FromForm(Name = "detailslink")]
        public string? DetailsLink { get; set; }

        [FromForm(Name = "demolink")]
        public string? DemoLink { get; set; }
    }
}

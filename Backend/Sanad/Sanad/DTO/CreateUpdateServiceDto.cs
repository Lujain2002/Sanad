namespace Sanad.DTO
{
    public class CreateUpdateServiceDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public List<string>? Details { get; set; }
    }

}

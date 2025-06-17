namespace Sanad.Models
{
    public class ProductYear
    {
        public int Id { get; set; }
        public int YearValue { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}

using Sanad.Models;

namespace Sanad.Data.Seeders
{
    public static class CategoryYearSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "ai" },
                    new Category { Name = "health" },
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.ProductYears.Any())
            {
                var years = new List<ProductYear>
                {
                    new ProductYear { YearValue = 2023 },
                    new ProductYear { YearValue = 2024 },
                };

                context.ProductYears.AddRange(years);
                context.SaveChanges();
            }
        }
    }
}
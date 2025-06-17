using Sanad.Models;
using System.Text.Json;

namespace Sanad.Data.Seeders
{
    public static class ProductsSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Products.Any()) return;

            // Get categories and years
            var aiCategory = context.Categories.FirstOrDefault(c => c.Name == "ai");
            var healthCategory = context.Categories.FirstOrDefault(c => c.Name == "health");

            var year2023 = context.ProductYears.FirstOrDefault(y => y.YearValue == 2023);
            var year2024 = context.ProductYears.FirstOrDefault(y => y.YearValue == 2024);

            if (aiCategory == null || healthCategory == null || year2023 == null || year2024 == null)
                return; 

            var products = new List<Product>
            {
                new Product {
                    Title = "AI Chat Assistant",
                    Description = "Conversational AI for support and sales.",
                    LongDescription = "...",
                    CategoryId = aiCategory.Id,
                    ProductYearId = year2024.Id,
                    ImageUrl = "1.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "1.jpg", "2.jpg" }),
                    Tags = "AI,Chatbot,Automation",
                    BuyLink = "...", DetailsLink = "...", DemoLink = "..."
                },
                new Product {
                    Title = "Health Tracker",
                    Description = "Real-time health analytics and wellness.",
                    LongDescription = "...",
                    CategoryId = healthCategory.Id,
                    ProductYearId = year2023.Id,
                    ImageUrl = "2.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "2.jpg", "3.jpg" }),
                    Tags = "Health,Fitness,Tracker",
                    BuyLink = "...", DetailsLink = "...", DemoLink = "..."
                },
                new Product {
                    Title = "EduSmart Platform",
                    Description = "Interactive AI learning tools.",
                    LongDescription = "...",
                    CategoryId = aiCategory.Id,
                    ProductYearId = year2024.Id,
                    ImageUrl = "3.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "3.jpg", "4.jpg" }),
                    Tags = "Education,Platform,AI",
                    BuyLink = "...", DetailsLink = "...", DemoLink = "..."
                },
                new Product {
                    Title = "AI Financial Analyst",
                    Description = "Portfolio and trend prediction.",
                    LongDescription = "...",
                    CategoryId = aiCategory.Id,
                    ProductYearId = year2024.Id,
                    ImageUrl = "5.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "5.jpg", "4.jpg" }),
                    Tags = "Finance,AI,Analytics",
                    BuyLink = "...", DetailsLink = "...", DemoLink = "..."
                },
                new Product {
                    Title = "Smart Clinic",
                    Description = "AI-integrated clinic management system.",
                    LongDescription = "...",
                    CategoryId = healthCategory.Id,
                    ProductYearId = year2023.Id,
                    ImageUrl = "images.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "3.jpg", "4.jpg" }),
                    Tags = "HealthTech,Clinic,System",
                    BuyLink = "...", DetailsLink = "...", DemoLink = "..."
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}

using Sanad.Models;
using System.Text.Json;

namespace Sanad.Data.Seeders
{
    public class ProductsSeeders
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product {
                    Title = "AI Chat Assistant",
                    Description = "Conversational AI for support and sales.",
                    LongDescription = "AI-driven marketing strategies, smart advertising campaigns, and SEO & Ads optimization to maximize conversion rates.",
                    Year = 2024,
                    Category = "ai",
                    ImageUrl = "1.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "1.jpg", "2.jpg" }),
                    Tags = "AI,Chatbot,Automation",
                    BuyLink = "https://example.com/buy?id=1",
                    DetailsLink = "https://example.com/details?id=1",
                    DemoLink = "https://example.com/demo?id=1"
                },
                new Product {
                    Title = "Health Tracker",
                    Description = "Real-time health analytics and wellness.",
                    LongDescription = "AI-driven marketing strategies, smart advertising campaigns, and SEO & Ads optimization to maximize conversion rates.",
                    Year = 2023,
                    Category = "health",
                    ImageUrl = "2.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "2.jpg", "3.jpg" }),
                    Tags = "Health,Fitness,Tracker",
                    BuyLink = "https://example.com/buy?id=2",
                    DetailsLink = "https://example.com/details?id=2",
                    DemoLink = "https://example.com/demo?id=2"
                },
                new Product {
                    Title = "EduSmart Platform",
                    Description = "Interactive AI learning tools.",
                    LongDescription = "AI-driven marketing strategies, smart advertising campaigns, and SEO & Ads optimization to maximize conversion rates.",
                    Year = 2024,
                    Category = "ai",
                    ImageUrl = "3.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "3.jpg", "4.jpg" }),
                    Tags = "Education,Platform,AI",
                    BuyLink = "https://example.com/buy?id=3",
                    DetailsLink = "https://example.com/details?id=3",
                    DemoLink = "https://example.com/demo?id=3"
                },
                new Product {
                    Title = "AI Financial Analyst",
                    Description = "Portfolio and trend prediction.",
                    LongDescription = "AI-driven marketing strategies, smart advertising campaigns, and SEO & Ads optimization to maximize conversion rates.",
                    Year = 2024,
                    Category = "ai",
                    ImageUrl = "5.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "5.jpg", "4.jpg" }),
                    Tags = "Finance,AI,Analytics",
                    BuyLink = "https://example.com/buy?id=4",
                    DetailsLink = "https://example.com/details?id=4",
                    DemoLink = "https://example.com/demo?id=4"
                },
                new Product {
                    Title = "Smart Clinic",
                    Description = "AI-integrated clinic management system.",
                    LongDescription = "AI-driven marketing strategies, smart advertising campaigns, and SEO & Ads optimization to maximize conversion rates.",
                    Year = 2023,
                    Category = "health",
                    ImageUrl = "images.jpg",
                    Thumbnails = JsonSerializer.Serialize(new List<string> { "3.jpg", "4.jpg" }),
                    Tags = "HealthTech,Clinic,System",
                    BuyLink = "https://example.com/buy?id=5",
                    DetailsLink = "https://example.com/details?id=5",
                    DemoLink = "https://example.com/demo?id=5"
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}

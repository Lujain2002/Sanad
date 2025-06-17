using Sanad.Models;

namespace Sanad.Data.Seeders
{
    public class PartnerSeeders
    {
        public static void SeedPartners(ApplicationDbContext context)
        {
            if (!context.Partners.Any())
            {
                context.Partners.AddRange(
                    new Partner
                    {
                        Name = "Microsoft",
                        LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/4/44/Microsoft_logo.svg",
                        WebsiteUrl = "https://microsoft.com"
                    },
                    new Partner
                    {
                        Name = "Apple",
                        LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg",
                        WebsiteUrl = "https://apple.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}


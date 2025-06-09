
using Sanad.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sanad.Data.Seeders
{
    public class ServicesSeeders
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Services.Any()) return;

            var services = new List<Service>
            {
                new Service
                {
                    Title = "AI-Powered Digital Marketing",
                    Description = "Data-driven strategies and performance measurement.",
                    Details = new List<string>
                    {
                        "AI-Driven Strategy Development: We create data-driven marketing strategies tailored to your goals...",
                        "Smart Advertising Campaigns: Managing AI-powered ad campaigns across platforms...",
                        "SEO and SEM Optimization: Applying machine learning techniques..."
                    }
                },
                new Service
                {
                    Title = "Website & App Development",
                    Description = "Scalable web and app solutions with AI personalization.",
                    Details = new List<string>
                    {
                        "We build scalable websites, applications, and e-commerce platforms with intelligent features.",
                        "Integrating chatbots, personalization systems, and user behavior tracking.",
                        "Security monitoring through AI systems ensures full protection."
                    }
                },
                new Service
                {
                    Title = "Graphic Design and Animation",
                    Description = "Professional visuals with AI tools.",
                    Details = new List<string>
                    {
                        "Professional infographics powered by smart design tools...",
                        "Producing animations with cutting-edge techniques...",
                        "Integrating these elements into campaigns..."
                    }
                },
                new Service
                {
                    Title = "Domain Registration and Hosting",
                    Description = "Secure and scalable hosting with domain registration.",
                    Details = new List<string>
                    {
                        "Providing domain registration services aligned with your brand identity.",
                        "Hosting services include intelligent threat detection and high availability mechanisms.",
                        "24/7 technical monitoring ensures service continuity and security."
                    }
                },
                new Service
                {
                    Title = "Business Development and Strategic Consulting",
                    Description = "Smart analysis and strategic planning with AI support.",
                    Details = new List<string>
                    {
                        "Smart Business Analysis: Utilizing research tools, data analytics, and intelligent modeling to diagnose challenges and design effective growth strategies.",
                        "Process automation solutions increase efficiency and reduce costs.",
                        "Predictive insights guide decision-making and support market expansion."
                    }
                },
                new Service
                {
                    Title = "AI-Powered Project Management",
                    Description = "Agile and waterfall methods with intelligent dashboards.",
                    Details = new List<string>
                    {
                        "Implementing Agile and Waterfall methodologies backed by smart planning and risk analysis tools.",
                        "Real-time dashboards track progress, task distribution, and bottlenecks.",
                        "Continuous follow-up and feedback ensure goal alignment and stakeholder expectations."
                    }
                },
                new Service
                {
                    Title = "Management Information Systems (MIS)",
                    Description = "AI-powered MIS with automation and smart monitoring.",
                    Details = new List<string>
                    {
                        "Developing AI-powered management information platforms to digitize, link, and optimize processes.",
                        "Automated reports, smart alerts, and performance monitoring enhance decision-making efficiency.",
                        "High-performance, accurate systems support reliability, compliance, and scalability."
                    }
                },
                new Service
                {
                    Title = "Data Analysis and Visualization",
                    Description = "Transforming data into actionable insights.",
                    Details = new List<string>
                    {
                        "Transforming raw data into strategic insights through intelligent analysis and modeling.",
                        "Designing interactive dashboards to monitor performance and detect recurring patterns.",
                        "Customized solutions to empower decision-makers with clear, real-time information."
                    }
                },
                new Service
                {
                    Title = "Intelligent Process Documentation",
                    Description = "AI-supported tools for documenting and managing processes.",
                    Details = new List<string>
                    {
                        "Using AI-supported documentation tools for efficient capture and update of procedures.",
                        "Automatic updates and contextual tagging ensure version control and easy access.",
                        "Ideal for training, standardizing procedures, and regulatory compliance."
                    }
                }
            };

            context.Services.AddRange(services);
            context.SaveChanges();
        }
    }
}
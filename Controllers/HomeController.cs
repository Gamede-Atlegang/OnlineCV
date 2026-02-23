using GamedeCV.Models;
using GamedeCV.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamedeCV.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>Main portfolio page (single-page CV).</summary>
        public IActionResult Index()
        {
            var model = GetPortfolioData();
            return View(model);
        }

        /// <summary>Download CV as PDF (generated from your portfolio data).</summary>
        public IActionResult DownloadPdf()
        {
            var model = GetPortfolioData();
            var bytes = CvDocumentGenerator.GeneratePdf(model);
            var fileName = $"{model.FullName.Replace(" ", "_")}_CV.pdf";
            return File(bytes, "application/pdf", fileName);
        }

        /// <summary>Download CV as Word document (generated from your portfolio data).</summary>
        public IActionResult DownloadWord()
        {
            var model = GetPortfolioData();
            var bytes = CvDocumentGenerator.GenerateWord(model);
            var fileName = $"{model.FullName.Replace(" ", "_")}_CV.docx";
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Portfolio/CV data. Used by the web page and by DownloadPdf / DownloadWord.
        /// </summary>
        private static PortfolioViewModel GetPortfolioData()
        {
            return new PortfolioViewModel
            {
                FullName = "Atlegang Gamede",
                ProfessionalTitle = "Final-Year IT Student · Aspiring Software Developer",
                AboutMe = "I am a final-year IT student with a focus on software development. I enjoy building web applications and exploring new technologies. I am passionate about clean code, user experience, and continuous learning.",

                SkillGroups = new List<SkillGroup>
            {
                new() { Title = "Languages", Skills = new List<string> { "Java", "C#" } },
                new() { Title = "Frameworks", Skills = new List<string> { "ASP.NET", "React", "Netbeans" } },
                new() { Title = "Tools", Skills = new List<string> { "Git", "Docker", "VS Code", "Postman" } }
            },

                Projects = new List<ProjectItem>
            {
                new()
                {
                    Name = "CyberBot - Cybersecurity Awareness Assistant",
                    Description = "CyberBot is an interactive console application designed to educate users about cybersecurity best practices. It provides personalized guidance on topics such as password safety, phishing awareness, and safe browsing habits.",
                    TechStack = "C# + .NET + Visual Studio (Console App)",
                    GitHubUrl = "https://github.com/IIEMSA/prog6221-poe-Gamede-Atlegang.git"
                },
                new()
                {
                    Name = "ABC Retailers - Azure Storage Integration",
                    Description = "A comprehensive retail management web application demonstrating full integration with Azure Storage services.",
                    TechStack = "ASP.NET Core MVC + C# + Razor + static assets + Azure Functions + Azure",
                    GitHubUrl = "https://github.com/Gamede-Atlegang/ABC-Retailers-Project.git"
                },
                new()
                {
                    Name = "Contract Monthly Claim System (CMCS)",
                    Description = "The Contract Monthly Claim System (CMCS) is an ASP.NET Core MVC web application designed to manage monthly claims submitted by contract lecturers",
                    TechStack = "C# + .NET + Visual Studio",
                    GitHubUrl = "https://github.com/IIEMSA/prog6212-part1-Gamede-Atlegang.git"
                }
            },

                Degree = "Bachelor of Computer and Information Technology in Application Development",
                Institution = "Emeris Ruimsig",
                ExpectedGraduation = "2026",

                HighSchoolName = "Covenant Collage",
                HighSchoolGraduationYear = "2023",

                Email = "atl.gamed21@gmail.com",
                LinkedInUrl = "https://www.linkedin.com/in/atlegang-gamede-b891b03b3",
                GitHubUrl = "https://github.com/Gamede-Atlegang",
                OtherProfileLinks = new List<ProfileLink>(),

                Experience = new List<ExperienceItem>(),
                Certifications = new List<CertificationItem>(),
                Interests = "Personal health, sports and entrepreneurship"
            };
        }
    }


using GamedeCV.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GamedeCV.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WpDocument = DocumentFormat.OpenXml.Wordprocessing.Document;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace GamedeCV.Services
{
    public class CvDocumentGenerator
    {
        static CvDocumentGenerator()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static byte[] GeneratePdf(PortfolioViewModel model)
        {
            var stream = new MemoryStream();
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1.5f, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Grey.Darken3));

                    page.Content().Column(column =>
                    {
                        column.Spacing(8);

                        // Name & title
                        column.Item().Text(model.FullName).Bold().FontSize(18).FontColor(Colors.Blue.Darken2);
                        column.Item().Text(model.ProfessionalTitle).FontSize(11).FontColor(Colors.Grey.Medium);
                        column.Item().PaddingBottom(6);

                        // About
                        column.Item().Text("About Me").Bold().FontSize(12);
                        column.Item().Text(model.AboutMe);
                        column.Item().PaddingBottom(6);

                        // Skills
                        column.Item().Text("Skills").Bold().FontSize(12);
                        foreach (var group in model.SkillGroups)
                        {
                            column.Item().Text($"{group.Title}: {string.Join(", ", group.Skills)}").FontSize(10);
                        }
                        column.Item().PaddingBottom(6);

                        // Projects
                        column.Item().Text("Projects").Bold().FontSize(12);
                        foreach (var p in model.Projects)
                        {
                            column.Item().Text(p.Name).SemiBold();
                            column.Item().Text(p.Description).FontSize(9);
                            column.Item().Text($"Tech: {p.TechStack}").FontSize(9).FontColor(Colors.Grey.Medium);
                        }
                        column.Item().PaddingBottom(6);

                        // Education
                        column.Item().Text("Education").Bold().FontSize(12);
                        column.Item().Text($"{model.Degree} — {model.Institution} (Expected: {model.ExpectedGraduation})");
                        if (!string.IsNullOrEmpty(model.HighSchoolName))
                        {
                            var hs = string.IsNullOrEmpty(model.HighSchoolGraduationYear)
                                ? model.HighSchoolName
                                : $"{model.HighSchoolName} (Graduated: {model.HighSchoolGraduationYear})";
                            column.Item().Text($"High School: {hs}");
                        }
                        column.Item().PaddingBottom(6);

                        // Interests (if present)
                        if (!string.IsNullOrEmpty(model.Interests))
                        {
                            column.Item().Text("Interests").Bold().FontSize(12);
                            column.Item().Text(model.Interests);
                            column.Item().PaddingBottom(6);
                        }

                        // Contact
                        column.Item().Text("Contact").Bold().FontSize(12);
                        column.Item().Text($"Email: {model.Email}");
                        column.Item().Text($"LinkedIn: {model.LinkedInUrl}");
                        column.Item().Text($"GitHub: {model.GitHubUrl}");
                    });
                });
            }).GeneratePdf(stream);

            return stream.ToArray();
        }

        public static byte[] GenerateWord(PortfolioViewModel model)
        {
            using var stream = new MemoryStream();
            using (var package = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                var mainPart = package.AddMainDocumentPart();
                mainPart.Document = new WpDocument();
                var body = mainPart.Document.AppendChild(new Body());

                void AddParagraph(string text, bool bold = false)
                {
                    var p = body.AppendChild(new Paragraph());
                    var run = p.AppendChild(new Run());
                    if (bold)
                        run.RunProperties = new RunProperties(new Bold());
                    run.AppendChild(new Text(text));
                }

                void AddHeading(string text)
                {
                    var p = body.AppendChild(new Paragraph());
                    var run = p.AppendChild(new Run());
                    run.RunProperties = new RunProperties(new Bold());
                    run.AppendChild(new Text(text));
                }

                AddHeading(model.FullName);
                AddParagraph(model.ProfessionalTitle);
                body.AppendChild(new Paragraph());

                AddParagraph("About Me", true);
                AddParagraph(model.AboutMe);
                body.AppendChild(new Paragraph());

                AddParagraph("Skills", true);
                foreach (var group in model.SkillGroups)
                    AddParagraph($"{group.Title}: {string.Join(", ", group.Skills)}");
                body.AppendChild(new Paragraph());

                AddParagraph("Projects", true);
                foreach (var p in model.Projects)
                {
                    AddParagraph(p.Name, true);
                    AddParagraph(p.Description);
                    AddParagraph($"Tech stack: {p.TechStack}");
                }
                body.AppendChild(new Paragraph());

                AddParagraph("Education", true);
                AddParagraph($"{model.Degree} — {model.Institution}");
                AddParagraph($"Expected graduation: {model.ExpectedGraduation}");
                if (!string.IsNullOrEmpty(model.HighSchoolName))
                {
                    AddParagraph($"High School: {model.HighSchoolName}");
                    if (!string.IsNullOrEmpty(model.HighSchoolGraduationYear))
                        AddParagraph($"Graduated: {model.HighSchoolGraduationYear}");
                }
                body.AppendChild(new Paragraph());

                if (!string.IsNullOrEmpty(model.Interests))
                {
                    AddParagraph("Interests", true);
                    AddParagraph(model.Interests);
                    body.AppendChild(new Paragraph());
                }

                AddParagraph("Contact", true);
                AddParagraph($"Email: {model.Email}");
                AddParagraph($"LinkedIn: {model.LinkedInUrl}");
                AddParagraph($"GitHub: {model.GitHubUrl}");

                mainPart.Document.Save();
            }

            return stream.ToArray();
        }
    }
}

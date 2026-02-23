namespace GamedeCV.Models
{
    public class PortfolioViewModel
    {
        // ----- Must-have: Header / Hero -----
        public string FullName { get; set; } = "Your Full Name";
        public string ProfessionalTitle { get; set; } = "Final-Year IT Student · Aspiring Software Developer";
        public string? AvatarPath { get; set; } = "~/images/avatar.jpg";

        // ----- Must-have: About Me -----
        public string AboutMe { get; set; } = "";

        // ----- Must-have: Skills (grouped: Languages, Frameworks, Tools) -----
        public List<SkillGroup> SkillGroups { get; set; } = new();

        // ----- Must-have: Projects (at least one GitHub project) -----
        public List<ProjectItem> Projects { get; set; } = new();

        // ----- Must-have: Education -----
        public string Degree { get; set; } = "";
        public string Institution { get; set; } = "";
        public string ExpectedGraduation { get; set; } = "";
        // ----- High school (graduation) -----
        public string? HighSchoolName { get; set; }
        public string? HighSchoolGraduationYear { get; set; }

        // ----- Must-have: Contact (email + LinkedIn minimum) -----
        public string Email { get; set; } = "";

        // ----- Profile / social links (active links for coders) -----
        public string LinkedInUrl { get; set; } = "";
        public string GitHubUrl { get; set; } = "";
        public List<ProfileLink> OtherProfileLinks { get; set; } = new();

        // ----- Optional: Downloadable PDF CV (ATS-friendly) -----
        public string? PdfCvPath { get; set; }

        // ----- Optional: Work Experience / Internships -----
        public List<ExperienceItem> Experience { get; set; } = new();

        // ----- Optional: Certifications -----
        public List<CertificationItem> Certifications { get; set; } = new();

        // ----- Optional: Interests (brief line) -----
        public string? Interests { get; set; }
    }

    public class SkillGroup
    {
        public string Title { get; set; } = "";
        public List<string> Skills { get; set; } = new();
    }

    /// <summary>Optional profile link (e.g. Stack Overflow, portfolio, Twitter).</summary>
    public class ProfileLink
    {
        public string Label { get; set; } = "";
        public string Url { get; set; } = "";
    }
}

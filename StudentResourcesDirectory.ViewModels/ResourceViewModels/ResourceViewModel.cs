using StudentResourcesDirectory.Data.Models.Enums;

namespace StudentResourcesDirectory.ViewModels.ResourceViewModels
{
    public class ResourceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Student { get; set; } = null!;
        public int StudentId { get; set; }
        public string Url { get; set; } = null!;
        public ResourceType ResourceType { get; set; }
    }
}
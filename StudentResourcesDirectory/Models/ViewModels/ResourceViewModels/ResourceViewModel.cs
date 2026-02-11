using StudentResourcesDirectory.Models.Enums;

namespace StudentResourcesDirectory.Models.ViewModels.ResourceViewModels
{
    public class ResourceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Student { get; set; } = null!;
        public string Url { get; set; } = null!;
        public ResourceType ResourceType { get; set; }
    }
}
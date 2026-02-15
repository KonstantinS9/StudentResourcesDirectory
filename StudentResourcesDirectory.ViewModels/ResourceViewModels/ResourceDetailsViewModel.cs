using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Data.Models.Enums;
namespace StudentResourcesDirectory.ViewModels.ResourceViewModels
{
    public class ResourceDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        public ResourceType ResourceType { get; set; }

        public Category Category { get; set; } = null!;
        public Student Student { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}


using StudentResourcesDirectory.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static StudentResourcesDirectory.GCommon.EntityValidation.Resource;
namespace StudentResourcesDirectory.ViewModels.AdminViewModels.ResourceManagementViewModels
{
    public class ResourceManagementViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ResourceTitleMaxLength)]
        [MinLength(ResourceTitleMinLength)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(ResourceDescriptionMaxLength)]
        [MinLength(ResourceDescriptionMinLength)]
        public string Description { get; set; } = null!;
        [Required]
        [Url]
        public string Url { get; set; } = null!;
        [Required]
        public ResourceType ResourceType { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}

using StudentResourcesDirectory.Data.Models.Enums;

namespace StudentResourcesDirectory.ViewModels.RatingViewModels
{
    public class RatingIndexViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public RatingResource RatingResource { get; set; }
    }
}
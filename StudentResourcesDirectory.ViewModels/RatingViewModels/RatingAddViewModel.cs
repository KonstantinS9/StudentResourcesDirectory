

using StudentResourcesDirectory.Data.Models.Enums;

namespace StudentResourcesDirectory.ViewModels.RatingViewModels
{
    public class RatingAddViewModel
    {
        public int ResourceId { get; set; }
        public RatingResource RatingResource { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DemoProjectWebsite.Models
{
    public class MoreInfoModel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }   // Link to Identity user

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Ethnicity { get; set; }

        [Required]
        public string HairColor { get; set; }

        [Required]
        public int YearOfBirth { get; set; }
    }
}

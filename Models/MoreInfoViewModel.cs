using System.ComponentModel.DataAnnotations;

namespace DemoProjectWebsite.Models
{
    public class MoreInfoViewModel
    {
        [Required]
        public string Gender { get; set; }

        [Required]
        public string Ethnicity { get; set; }

        [Required]
        public string HairColor { get; set; }

        [Required]
        [Display(Name = "Year of Birth")]
        public int YearOfBirth { get; set; }
    }
}


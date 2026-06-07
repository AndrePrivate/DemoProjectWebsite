using System.ComponentModel.DataAnnotations;

namespace DemoProjectWebsite.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Message { get; set; }
    }
}

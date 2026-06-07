using System.ComponentModel.DataAnnotations;

namespace DemoProjectWebsite.Models
{
    public class UserModel
    {
        public string Id { get; set; }              // Identity UserId
        public string FullName { get; set; }        // From Contact table
        public string Email { get; set; }           // From Identity
        public string Phone { get; set; }           // From Contact table
        public string Gender { get; set; }          // From MoreInfo table
        public string Ethnicity { get; set; }       // From MoreInfo table
        public string HairColor { get; set; }       // From MoreInfo table
        public int YearOfBirth { get; set; }        // From MoreInfo table
    }
}

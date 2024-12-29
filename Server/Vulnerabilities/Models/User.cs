using System.ComponentModel.DataAnnotations;

namespace Vulnerabilities.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }    

        public Profile Profiles { get; set; }
        public List<Comment> Comments { get; set; }

    }
}

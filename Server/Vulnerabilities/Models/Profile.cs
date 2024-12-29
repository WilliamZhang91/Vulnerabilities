namespace Vulnerabilities.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public User Users { get; set; }
        public ICollection<CreditCard>? CreditCards { get; set; }
    }
}

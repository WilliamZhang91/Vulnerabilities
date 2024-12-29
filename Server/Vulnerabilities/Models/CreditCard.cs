using Vulnerabilities.Attirbutes;

namespace Vulnerabilities.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        [EncryptColumn]
        public string EncryptedCreditCardNumber { get; set; } = string.Empty;
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }
    }
}

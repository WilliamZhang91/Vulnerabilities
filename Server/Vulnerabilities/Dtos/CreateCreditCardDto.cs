namespace Vulnerabilities.Dtos
{
    public class CreateCreditCardDto
    {
        public string EncryptedCreditCardNumber { get; set; }
        public int ProfileId { get; set; }
    }
}

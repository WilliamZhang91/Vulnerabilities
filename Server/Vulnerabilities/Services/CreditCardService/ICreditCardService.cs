using Vulnerabilities.Models;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Services.CreditCardService
{
    public interface ICreditCardService
    {
        Task<CreditCard> CreateCreditCardAsync(CreateCreditCardDto creditCardDto);
        Task<List<CreditCard>> GetCreditCardsAsync(int profileId);
    }
}

using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Repositories.CreditCardRepository
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> CreateCreditCardAsync(CreateCreditCardDto creditCardDto);
        Task<List<CreditCard>> GetAllCreditCardsAsync(int profileId);    
    }
}

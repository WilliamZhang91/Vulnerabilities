using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Vulnerabilities.Repositories.CreditCardRepository;

namespace Vulnerabilities.Services.CreditCardService
{
    public class CreditCardService: ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ILogger<ICreditCardService> _logger;   

        public CreditCardService(ICreditCardRepository creditCardRepository, ILogger<ICreditCardService> logger)
        {
            _creditCardRepository = creditCardRepository;
            _logger = logger;
        }

        public async Task<List<CreditCard>> GetCreditCardsAsync(int profileId)
        {
            try
            {
                var crediCards = await _creditCardRepository.GetAllCreditCardsAsync(profileId);
                return crediCards;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CreditCard> CreateCreditCardAsync(CreateCreditCardDto creditCardDto)
        {
            try
            {
                var createdCreditCard = await _creditCardRepository.CreateCreditCardAsync(creditCardDto);
                return createdCreditCard;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.InnerException?.ToString());
                throw;
            }
        }
    }
}

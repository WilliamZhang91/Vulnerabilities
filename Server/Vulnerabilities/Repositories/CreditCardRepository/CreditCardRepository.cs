using Microsoft.EntityFrameworkCore;
using Vulnerabilities.Data;
using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Vulnerabilities.Services.EncryptionProvider;

namespace Vulnerabilities.Repositories.CreditCardRepository
{
    public class CreditCardRepository: ICreditCardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionProvider _encryptionProvider;

        public CreditCardRepository(ApplicationDbContext context, IEncryptionProvider encryptionProvider)
        {
            _context = context;
            _encryptionProvider = encryptionProvider;
        }

        public async Task<List<CreditCard>> GetAllCreditCardsAsync(int profileId)
        {
            string query = "SELECT * FROM CreditCards WHERE Id = " + profileId;
            var creditCards = await _context.CreditCards
                .FromSqlRaw(query)
                .ToListAsync();

            foreach (var creditCard in creditCards)
            {
                creditCard.EncryptedCreditCardNumber = _encryptionProvider.Decrypt(creditCard.EncryptedCreditCardNumber);
            }

            return creditCards;
        }

        public async Task<CreditCard> CreateCreditCardAsync(CreateCreditCardDto creditCardDto)
        {
            string encryptedCreditCardNumber = _encryptionProvider.Encrypt(creditCardDto.EncryptedCreditCardNumber);

            var creditCard = new CreditCard 
            {
                EncryptedCreditCardNumber = encryptedCreditCardNumber,
                ProfileId = creditCardDto.ProfileId, 
            };

            await _context.CreditCards.AddAsync(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
        }
    }
}

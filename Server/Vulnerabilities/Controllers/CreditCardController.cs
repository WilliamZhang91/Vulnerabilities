using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Vulnerabilities.Services.CreditCardService;

namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService; 
        }

        [HttpGet("Vulnerable")]
        public async Task<IActionResult> GetCreditCards(int profileId)
        {
            try
            {
                var creditCards = await _creditCardService.GetCreditCardsAsync(profileId);
                return Ok(creditCards);
            }
            catch
            {
                return StatusCode(500);
            }  
        }

        [HttpPost("Vulnerable")]
        public async Task<IActionResult> CreateCreditCard([FromBody] CreateCreditCardDto creditCardDto)
        {
            try
            {
                var createdCreditCard = await _creditCardService.CreateCreditCardAsync(creditCardDto);
                return Ok(createdCreditCard);
            }
            catch 
            {
                return StatusCode(500);
            }
        }
    }
}

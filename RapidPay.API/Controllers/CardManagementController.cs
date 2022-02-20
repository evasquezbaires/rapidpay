using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Models;
using RapidPay.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RapidPay.API.Controllers
{
    /// <summary>
    /// Handles operational functions about new Credit cards and their payments
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardManagementController : ControllerBase
    {
        private readonly ICardManagementService _cardService;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "Fee";

        /// <summary>
        /// Main constructor of Card controller
        /// </summary>
        /// <param name="cardService">The service management for all functions</param>
        /// <param name="cache">The singleton cache for Fee exchange</param>
        public CardManagementController(ICardManagementService cardService, IMemoryCache cache)
        {
            _cardService = cardService;
            _cache = cache;
        }

        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCreditCard([FromBody] CreditCardModel modelRequest)
        {
            try
            {
                var result = await _cardService.CreateCreditCardAsync(modelRequest);
                var response = new ApiResponse { Data = new { CardId = result } };
                if (result > 0)
                    return Created(string.Empty, response);
                else
                    return NotFound(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse { Errors = new List<string> { ex.Message } });
            }
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCardModel modelRequest)
        {
            try
            {
                var feeExchange = await GetFeeExchange();
                var result = await _cardService.CreatePaymentCardAsync(modelRequest, feeExchange);
                var response = new ApiResponse { Data = result };
                if (result != null)
                    return Created(string.Empty, response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Errors = new List<string> { ex.Message } });
            }
        }

        [HttpGet("GetBalance/{cardId}")]
        public async Task<ActionResult<decimal>> GetCurrentBalance([Required]int cardId)
        {
            try
            {
                var result = await _cardService.GetCardBalanceAsync(cardId);
                var response = new ApiResponse { Data = new { BalanceAmount = result } };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Errors = new List<string> { ex.Message } });
            }
        }

        private async Task<decimal> GetFeeExchange()
        {
            if (_cache.Get(CACHE_KEY) != null)
            {
                return (decimal)_cache.Get(CACHE_KEY);
            }
            else
            {
                var feeExchange = decimal.Round(GetRandom(), 2);
                SaveCache(feeExchange);
                return feeExchange;
            }
        }

        private void SaveCache(decimal feeExchange)
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
            // Save data in cache.
            _cache.Set(CACHE_KEY, feeExchange, cacheEntryOptions);
        }

        private decimal GetRandom()
        {
            var random = new Random();
            return (decimal)(random.NextDouble() * 2);
        }
    }
}

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Services
{
    public class FeeManagementService : IFeeManagementService
    {
        private readonly IMemoryCache _cache;
        private readonly IRapidPayRepository _repository;
        private readonly ILogger _logger;
        private const string CACHE_KEY = "Fee";

        public FeeManagementService(IMemoryCache cache, IRapidPayRepository repository, ILogger logger)
        {
            _cache = cache;
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<decimal> GetCurrentFeeRate()
        {
            if (_cache.Get(CACHE_KEY) != null)
            {
                return (decimal)_cache.Get(CACHE_KEY);
            }
            else
            {
                return await UpdateFeeRate();
            }
        }

        private async Task<decimal> UpdateFeeRate()
        {
            var feeExchange = GetRandom();
            if (_cache.Get(CACHE_KEY) == null)
            {
                var previousFee = await _repository.GetLastPaymentFeeAsync();
                if (previousFee != null)
                    feeExchange *= previousFee.FeeRate;

                SaveCache(feeExchange);
                await CreateNewPaymentFee(feeExchange);
            }

            return feeExchange;
        }

        private async Task CreateNewPaymentFee(decimal feeExchange)
        {
            var feeEntity = new FeeHistory { FeeRate = feeExchange };
            await _repository.AddPaymentFeeAsync(feeEntity);
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
            byte scale = (byte)random.Next(29);
            return new decimal(0, 1, 2, false, scale);
        }
    }
}

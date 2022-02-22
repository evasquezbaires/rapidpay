using AutoMapper;
using Microsoft.Extensions.Logging;
using RapidPay.API.Domain.Constants;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Exceptions;
using RapidPay.API.Domain.Models;
using System;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Services
{
    /// <summary>
    /// Implementation for the Management of Cards
    /// </summary>
    public class CardManagementService : ICardManagementService
    {
        private readonly IRapidPayRepository _repository;
        private readonly IAuthenticateService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Main constructor of Card service
        /// </summary>
        /// <param name="repository">The repository for DB communication</param>
        /// <param name="authService">Service to operate with user authenticated</param>
        /// <param name="mapper">The mapper instance</param>
        /// <param name="logger">The logger instance</param>
        public CardManagementService(IRapidPayRepository repository, IAuthenticateService authService,
            IMapper mapper, ILogger<CardManagementService> logger)
        {
            _repository = repository;
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> CreateCreditCardAsync(CreditCardModel cardModel)
        {
            if (cardModel == null) throw new ArgumentNullException(nameof(cardModel));
            if (await ExistCard(cardModel.CardNumber)) throw new ApiException(ErrorMessages.CARD_EXISTS);

            var entityMapped = _mapper.Map<CreditCard>(cardModel);
            entityMapped.CreatedBy = _authService.GetUserAuthenticated();
            return await _repository.AddCardAsync(entityMapped);
        }

        /// <inheritdoc/>
        public async Task<PaymentCardResponse> CreatePaymentCardAsync(PaymentCardModel paymentModel, decimal feeExchange)
        {
            if (paymentModel == null) throw new ArgumentNullException(nameof(paymentModel));

            var cardEntity = await _repository.FindCardByIdAsync(paymentModel.CardId);
            if (cardEntity == null) throw new ApiException(ErrorMessages.CARD_NOT_EXISTS);
            
            var entityMapped = await CompleteMapPaymentCardValues(paymentModel, feeExchange);
            if (!HasValidCardAmount(cardEntity, entityMapped)) throw new ApiException(ErrorMessages.AMOUNT_NOT_ENOUGH);

            entityMapped.CreatedBy = _authService.GetUserAuthenticated();
            var paymentId = await _repository.AddPaymentAsync(entityMapped);
            cardEntity.BalanceAmount -= (entityMapped.Amount + entityMapped.Fee);
            await _repository.UpdateCardAsync(cardEntity);

            return new PaymentCardResponse
            {
                BalanceAmount = cardEntity.BalanceAmount,
                CardId = entityMapped.CreditCardId,
                PaymentId = paymentId
            };
        }

        /// <inheritdoc/>
        public async Task<decimal> GetCardBalanceAsync(int cardId)
        {
            if (cardId <= 0) throw new ArgumentNullException(nameof(cardId));

            var cardEntity = await _repository.FindCardByIdAsync(cardId);
            if (cardEntity == null) throw new ApiException(ErrorMessages.CARD_NOT_EXISTS);

            return cardEntity.BalanceAmount;
        }

        private bool HasValidCardAmount(CreditCard cardEntity, PaymentCard paymentEntity)
        {
            return cardEntity.BalanceAmount >= (paymentEntity.Amount + paymentEntity.Fee);
        }

        private async Task<PaymentCard> CompleteMapPaymentCardValues(PaymentCardModel paymentModel, decimal feeExchange)
        {
            var entityMapped = _mapper.Map<PaymentCard>(paymentModel);
            var previousFeeRate = await _repository.GetLastPaymentFeeAsync();
            var feeRate = previousFeeRate != null ? previousFeeRate.FeeRate * feeExchange : feeExchange;
            entityMapped.Fee = decimal.Round(feeRate, 2);

            if (previousFeeRate == null || previousFeeRate.FeeExchange != feeExchange)
                await _repository.AddPaymentFeeAsync(new FeeHistory
                {
                    FeeRate = feeRate,
                    FeeExchange = feeExchange,
                    CreatedBy = _authService.GetUserAuthenticated()
                });

            return entityMapped;
        }

        private async Task<bool> ExistCard(string cardNumber)
        {
            var cardEntity = await _repository.FindCardByNumberAsync(cardNumber);

            return cardEntity != null;
        }
    }
}

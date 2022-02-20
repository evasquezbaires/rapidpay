using RapidPay.API.Domain.Models;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Contracts
{
    /// <summary>
    /// Interface to operate the Card functions
    /// </summary>
    public interface ICardManagementService
    {
        /// <summary>
        /// Creates a new Credit card
        /// </summary>
        /// <param name="cardModel">The credit card model to create</param>
        /// <returns>Id of the new card saved</returns>
        Task<int> CreateCreditCardAsync(CreditCardModel cardModel);

        /// <summary>
        /// Creates a new Payment with a Credit card
        /// </summary>
        /// <param name="paymentModel">The payment model to create</param>
        /// <param name="feeExchange">The current fee rate</param>
        /// <returns>Object with last data of the payment generated</returns>
        Task<PaymentCardResponse> CreatePaymentCardAsync(PaymentCardModel paymentModel, decimal feeExchange);

        /// <summary>
        /// Gets a specific Card balance amount finding by the card Id
        /// </summary>
        /// <param name="cardId">Id of the card to find the balance</param>
        /// <returns>Balance of credit card found</returns>
        Task<decimal> GetCardBalanceAsync(int cardId);
    }
}

using RapidPay.API.Domain.Entities;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Contracts
{
    /// <summary>
    /// Interface for Repository at Rapid Pay transactions
    /// </summary>
    public interface IRapidPayRepository
    {
        /// <summary>
        /// Save a new Credit card into repository
        /// </summary>
        /// <param name="entity">The credit card entity to save</param>
        /// <returns>Id of the new card saved</returns>
        Task<int> AddCardAsync(CreditCard entity);

        /// <summary>
        /// Save a new Payment with Credit card into repository
        /// </summary>
        /// <param name="entity">The payment entity to save</param>
        /// <returns>Id of the new payment generated</returns>
        Task<int> AddPaymentAsync(PaymentCard entity);

        /// <summary>
        /// Save a new Payment Fee percent for payments with Credit card
        /// </summary>
        /// <param name="entity">The payment fee entity to save</param>
        /// <returns>Id of the new Fee generated</returns>
        Task<int> AddPaymentFeeAsync(FeeHistory entity);

        /// <summary>
        /// Gets a specific Card finding by the card number
        /// </summary>
        /// <param name="cardNumber">Number of the card to find the record</param>
        /// <returns>Credit card entity found</returns>
        Task<CreditCard> FindCardByNumberAsync(string cardNumber);

        /// <summary>
        /// Gets a specific Card finding by the Id of card
        /// </summary>
        /// <param name="cardId">Id of the card to find the record</param>
        /// <returns>Credit card entity found</returns>
        Task<CreditCard> FindCardByIdAsync(int cardId);

        /// <summary>
        /// Gets the last Payment fee generated in the History
        /// </summary>
        /// <returns>Payment fee entity found with the Criteria</returns>
        Task<FeeHistory> GetLastPaymentFeeAsync();

        /// <summary>
        /// Update an existent Credit card entity into repository
        /// </summary>
        /// <param name="entity">The credit card entity with values to update</param>
        /// <returns>Id of the card updated</returns>
        Task<int> UpdateCardAsync(CreditCard entity);

        /// <summary>
        /// Register a new user of system
        /// </summary>
        /// <param name="entity">The user entity to save</param>
        /// <returns>Id of the new user generated</returns>
        Task<int> AddUserAsync(UserIdentity entity);

        /// <summary>
        /// Gets a specific User by Login process
        /// </summary>
        /// <param name="name">Username to find the record</param>
        /// <param name="password">Passwor to match with the user</param>
        /// <returns>Name of the user found</returns>
        Task<string> FindUserAsync(string name, string password);

        /// <summary>
        /// Gets a specific User to check existence
        /// </summary>
        /// <param name="name">Username to find the record</param>
        /// <returns>Id of the user found</returns>
        Task<string> FindUserByNameAsync(string name);
    }
}

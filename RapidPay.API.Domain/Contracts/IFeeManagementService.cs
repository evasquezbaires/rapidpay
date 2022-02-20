using System.Threading.Tasks;

namespace RapidPay.API.Domain.Contracts
{
    /// <summary>
    /// Interface to operate with Fee history and their update
    /// </summary>
    public interface IFeeManagementService
    {
        /// <summary>
        /// Get the updated Fee rate in Memory cache
        /// </summary>
        /// <returns>The fee exchange value</returns>
        Task<decimal> GetCurrentFeeRate();
    }
}

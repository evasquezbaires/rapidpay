using RapidPay.API.Domain.Models;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Contracts
{
    /// <summary>
    /// Interface to operate the Card functions
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="userModel">The user model to create</param>
        /// <returns>Id of the new user generated</returns>
        Task<int> CreateUserAsync(UserModel userModel);

        /// <summary>
        /// Gets a specific User from the Login process
        /// </summary>
        /// <param name="userModel">The user model to Login</param>
        /// <returns>Username of the user found</returns>
        Task<string> GetUserAsync(UserModel userModel);
    }
}

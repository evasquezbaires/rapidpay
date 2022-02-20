namespace RapidPay.API.Domain.Contracts
{
    /// <summary>
    /// Interface to operate the User authentication
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// Allow the system to perform the user authentication
        /// </summary>
        /// <param name="username">The name of the user to Authenticate</param>
        /// <returns>Token of user authenticated</returns>
        string AuthenticateUser(string username);

        /// <summary>
        /// Gets the name of the user in Session/Authenticated
        /// </summary>
        /// <returns></returns>
        string GetUserAuthenticated();
    }
}

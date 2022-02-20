using AutoMapper;
using Microsoft.Extensions.Logging;
using RapidPay.API.Domain.Constants;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Exceptions;
using RapidPay.API.Domain.Helpers;
using RapidPay.API.Domain.Models;
using System;
using System.Threading.Tasks;

namespace RapidPay.API.Domain.Services
{
    /// <summary>
    /// Implementation for the Management of Users
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly IRapidPayRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Main constructor of User service
        /// </summary>
        /// <param name="repository">The repository for DB communication</param>
        /// <param name="mapper">The mapper instance</param>
        /// <param name="logger">The logger instance</param>
        public UserManagementService(IRapidPayRepository repository, IMapper mapper, ILogger<UserManagementService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> CreateUserAsync(UserModel userModel)
        {
            if (userModel == null) throw new ArgumentNullException(nameof(userModel));
            var existentUser = await _repository.FindUserByNameAsync(userModel.Name);
            if (existentUser != null) throw new ApiException(ErrorMessages.USER_EXISTS);

            userModel.Password = EncryptionHelper.EncryptBase64(userModel.Password);
            var entityMapped = _mapper.Map<UserIdentity>(userModel);
            var userId = await _repository.AddUserAsync(entityMapped);

            return userId;
        }

        /// <inheritdoc/>
        public async Task<string> GetUserAsync(UserModel userModel)
        {
            if (userModel == null) throw new ArgumentNullException(nameof(userModel));
            var existentUser = await _repository.FindUserAsync(userModel.Name, EncryptionHelper.EncryptBase64(userModel.Password));
            if (string.IsNullOrEmpty(existentUser)) throw new ApiException(ErrorMessages.USER_NOT_EXISTS);

            return existentUser;
        }
    }
}

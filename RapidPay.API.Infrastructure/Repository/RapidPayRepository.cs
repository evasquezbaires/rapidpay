using Microsoft.EntityFrameworkCore;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Infrastructure.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.API.Infrastructure.Repository
{
    /// <summary>
    /// Implementation of Rapid pay Repository to handle communication with DB
    /// </summary>
    public class RapidPayRepository : IRapidPayRepository
    {
        private readonly RapidPayDBContext _context;
        private readonly Lazy<DbSet<CreditCard>> _dbSetCard;
        private readonly Lazy<DbSet<PaymentCard>> _dbSetPayment;
        private readonly Lazy<DbSet<FeeHistory>> _dbSetFee;
        private readonly Lazy<DbSet<UserIdentity>> _dbSetUser;

        /// <summary>
        /// Main constructor for repository
        /// </summary>
        /// <param name="context">The Context to connect to DB</param>
        public RapidPayRepository(RapidPayDBContext context)
        {
            _context = context;
            _dbSetCard = new Lazy<DbSet<CreditCard>>(() => _context.Set<CreditCard>());
            _dbSetPayment = new Lazy<DbSet<PaymentCard>>(() => _context.Set<PaymentCard>());
            _dbSetFee = new Lazy<DbSet<FeeHistory>>(() => _context.Set<FeeHistory>());
            _dbSetUser = new Lazy<DbSet<UserIdentity>>(() => _context.Set<UserIdentity>());
        }

        /// <inheritdoc/>
        public async Task<int> AddCardAsync(CreditCard entity)
        {
            await _dbSetCard.Value.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<int> AddPaymentAsync(PaymentCard entity)
        {
            await _dbSetPayment.Value.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<int> AddPaymentFeeAsync(FeeHistory entity)
        {
            await _dbSetFee.Value.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<CreditCard> FindCardByNumberAsync(string cardNumber)
        {
            return await _dbSetCard.Value.FirstOrDefaultAsync(q => q.CardNumber.Equals(cardNumber));
        }

        /// <inheritdoc/>
        public async Task<CreditCard> FindCardByIdAsync(int cardId)
        {
            return await _dbSetCard.Value.FirstOrDefaultAsync(q => q.Id == cardId);
        }

        /// <inheritdoc/>
        public async Task<FeeHistory> GetLastPaymentFeeAsync()
        {
            return await _dbSetFee.Value.OrderByDescending(q => q.CreatedDate).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateCardAsync(CreditCard entity)
        {
            await Task.Run(() => _dbSetCard.Value.Update(entity));
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<int> AddUserAsync(UserIdentity entity)
        {
            await _dbSetUser.Value.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<string> FindUserAsync(string name, string password)
        {
            var user = await _dbSetUser.Value.FirstOrDefaultAsync(q => q.Name.Equals(name) && q.Password.Equals(password));
            return user?.Name;
        }

        /// <inheritdoc/>
        public async Task<string> FindUserByNameAsync(string name)
        {
            var user = await _dbSetUser.Value.FirstOrDefaultAsync(q => q.Name.Equals(name));
            return user?.Name;
        }
    }
}

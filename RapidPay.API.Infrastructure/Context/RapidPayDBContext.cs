using Microsoft.EntityFrameworkCore;
using RapidPay.API.Domain.Entities;

#nullable disable

namespace RapidPay.API.Infrastructure.Context
{
    public partial class RapidPayDBContext : DbContext
    {
        public RapidPayDBContext(DbContextOptions<RapidPayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<FeeHistory> FeeHistories { get; set; }
        public virtual DbSet<PaymentCard> PaymentCards { get; set; }
        public virtual DbSet<UserIdentity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.ToTable("CreditCard");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CardHolder)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.Cvv)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("CVV");

                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<FeeHistory>(entity =>
            {
                entity.ToTable("FeeHistory");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.FeeRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FeeExchange).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PaymentCard>(entity =>
            {
                entity.ToTable("PaymentCard");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Fee).HasColumnType("money");

                entity.HasOne(d => d.CreditCard)
                    .WithMany(p => p.PaymentCards)
                    .HasForeignKey(d => d.CreditCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayCard_CreditCard");
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.ToTable("UserIdentity");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

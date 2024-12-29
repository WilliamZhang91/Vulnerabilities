using Microsoft.EntityFrameworkCore;
using Vulnerabilities.Models;
using Vulnerabilities.Services.EncryptionProvider;
using Vulnerabilities.Utilities;

namespace Vulnerabilities.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ApplicationDbContext(DbContextOptions options, IEncryptionProvider encryptionProvider) : base(options)
        {
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Profiles)
                .WithOne(p => p.Users)
                .HasForeignKey<Profile>(p => p.Id);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

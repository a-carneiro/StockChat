using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockChat.Domain.Entity;

namespace StockChat.Repository.Infrastructure
{
    public class StockChatDbContext : IdentityDbContext<User>
    {
        public StockChatDbContext(DbContextOptions<StockChatDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Chat>()
                .Property(x => x.Id).HasMaxLength(36);
            modelBuilder.Entity<Chat>()
                .Property(x => x.Name).HasMaxLength(10);

            modelBuilder.Entity<UserChat>()
                .HasKey(x => new { x.ChatId, x.UserId });

            modelBuilder.Entity<Message>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Message>()
                .Property(x => x.Id).HasMaxLength(36);
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
    }
}

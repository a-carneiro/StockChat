using EndpointManager.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EndpointManager.Repository.Infrastructure
{
    public class EndpointManagerContext : DbContext, IDbContext
    {
        public EndpointManagerContext(DbContextOptions<EndpointManagerContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endpoint>()
                .HasKey(x => x.SeriaNumber);

            modelBuilder.Entity<Endpoint>()
                .HasOne(x => x.Model);

            modelBuilder.Entity<MeterModel>()
                .HasKey(x => x.Id);
        }

        public virtual DbSet<Endpoint> Endpoints { get; set; }
        public virtual DbSet<MeterModel> Models { get; set; }
    }
}

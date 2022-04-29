using EndpointManager.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EndpointManager.Repository.Infrastructure
{
    public interface IDbContext
    {
       DbSet<Endpoint> Endpoints { get; set; }
       DbSet<MeterModel> Models { get; set; }

        int SaveChanges();
    }
}

using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TekConf.Data.Models;

namespace TekConf.Data
{
    public interface ITekConfContext
    {
        IDbSet<Conference> Conferences { get; set; }
        IDbSet<ConferenceSocialConnection> ConferenceSocialConnections { get; set; }
        IDbSet<Location> Locations { get; set; }
        IDbSet<Prerequisite> Prerequisites { get; set; }
        IDbSet<Room> Rooms { get; set; }
        IDbSet<Session> Sessions { get; set; }
        IDbSet<SessionLink> SessionLinks { get; set; }
        IDbSet<SessionSocialConnection> SessionSocialConnections { get; set; }
        IDbSet<SessionType> SessionTypes { get; set; }
        IDbSet<Tag> Tags { get; set; }

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
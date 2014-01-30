using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;
using TekConf.Data.Models;

namespace TekConf.Data
{

    public class TekConfContext : IdentityDbContext<User>, ITekConfContext
    {
        public TekConfContext() : base("TekConf") { }

        public IDbSet<Conference> Conferences { get; set; }
        public IDbSet<ConferenceSocialConnection> ConferenceSocialConnections { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Prerequisite> Prerequisites { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<Session> Sessions { get; set; }
        public IDbSet<SessionLink> SessionLinks { get; set; }
        public IDbSet<SessionSocialConnection> SessionSocialConnections { get; set; }
        public IDbSet<SessionType> SessionTypes { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().HasKey<string>(l => l.Id);
            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<Conference>().
                HasMany(c => c.AdminUsers).
                WithMany(p => p.ConferencesCreated).
                Map(
                    m =>
                    {
                        m.MapLeftKey("ConferenceId");
                        m.MapRightKey("UserId");
                        m.ToTable("ConferenceAdmins");
                    });
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
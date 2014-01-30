using System;
using System.Collections.Generic;
using System.Linq;

namespace TekConf.Data.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Tagline { get; set; }
        public bool IsLive { get; set; }
        public bool? IsOnline { get; set; }
        public bool? WillProvideVideos { get; set; }
        public int? DefaultTalkLength { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public DateTime? CallForSpeakersOpens { get; set; }
        public DateTime? CallForSpeakersCloses { get; set; }
        public DateTime? CreatedDate { get; set; }

        // One-to-One
        public virtual Location Location { get; set; }
        public virtual ConferenceSocialConnection SocialConnection { get; set; }

        // One-to-Many
        public virtual ICollection<SessionType> SessionTypes { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<User> AdminUsers { get; set; }

        public void AddAdminUser(User user)
        {
            if (this.AdminUsers == null)
            {
                AdminUsers = new HashSet<User>();
            }

            if (AdminUsers.All(u => u.Id != user.Id))
            {
                AdminUsers.Add(user);
            }
        }
    }
}

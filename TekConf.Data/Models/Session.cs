using System;
using System.Collections.Generic;

namespace TekConf.Data.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public Difficulty Difficulty { get; set; }
        
        //One-to-one
        public virtual Room Room { get; set; }
        public virtual SessionSocialConnection SocialConnection { get; set; }
        public virtual SessionType SessionType { get; set; }
        
        //One-to-many
        public virtual ICollection<SessionLink> Links { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Prerequisite> Prerequisites { get; set; }

        
        public virtual Conference Conference { get; set; }

        //Speakers
    }
}
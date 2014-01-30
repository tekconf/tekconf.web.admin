using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TekConf.Data.Models
{
    public class Role : IdentityRole
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        public virtual ICollection<User> Users { get; set; }
    }
}
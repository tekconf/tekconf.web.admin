using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TekConf.Data.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Conference> ConferencesCreated { get; set; } 
    }
}
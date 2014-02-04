using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using TekConf.Web.Admin.Code;

namespace TekConf.Web.Admin.ViewModels.Session
{
    public class SessionCreateViewModel
    {
        public CreateSessionDto Session { get; set; }
        public string ConferenceName { get; set; }
        public int ConferenceId { get; set; }
    }
}
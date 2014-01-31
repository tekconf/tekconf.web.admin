using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using TekConf.Web.Admin.Code;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class ConferenceCreateViewModel
    {
        public CreateConferenceDto Conference { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
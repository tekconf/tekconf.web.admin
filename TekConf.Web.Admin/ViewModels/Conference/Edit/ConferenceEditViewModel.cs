using System.Web;
using System.Web.Routing;
using TekConf.Web.Admin.Code;
using TekConf.Web.Admin.ViewModels.Conference;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class ConferenceEditViewModel : ViewModelBase
    {
        public ConferenceEditViewModel() : base() { }
        public ConferenceEditViewModel(RouteData routeData) : base(routeData) { }

        public EditConferenceDto Conference { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
				public HttpPostedFileBase SquareImageFile { get; set; }
    }
}
using System.Web;
using TekConf.Web.Admin.ViewModels.Conference;

namespace TekConf.Web.Admin.ViewModels.Conference
{
	public class ConferenceEditViewModel
	{
		public EditConferenceDto Conference { get; set; }
		public HttpPostedFileBase ImageFile { get; set; }
	}
}
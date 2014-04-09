using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;
using TekConf.Web.ViewModels;

namespace TekConf.Web.Controllers
{
	public class SpeakersController : ControllerBase
	{
		public SpeakersController(ITekConfContext context, ILog log) : base(context, log) { }

		public ActionResult Index()
		{
			var viewModel = new SpeakersViewModel();
			return View(viewModel);
		}
	}
}
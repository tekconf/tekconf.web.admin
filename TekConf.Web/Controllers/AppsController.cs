using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;

namespace TekConf.Web.Controllers
{
	public class AppsController : ControllerBase
	{
		public AppsController(ITekConfContext context, ILog log) : base(context, log) { }

		public ActionResult Index()
		{
			return View();
		}
	}
}
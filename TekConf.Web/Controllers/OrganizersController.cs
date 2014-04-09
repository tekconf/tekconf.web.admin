using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;

namespace TekConf.Web.Controllers
{
	public class OrganizersController : ControllerBase
	{
		public OrganizersController(ITekConfContext context, ILog log) : base(context, log) { }

		public ActionResult Index()
		{
			return View();
		}
	}
}
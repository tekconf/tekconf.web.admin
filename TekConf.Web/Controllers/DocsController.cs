using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;

namespace TekConf.Web.Controllers
{
	public class DocsController : ControllerBase
	{
		public DocsController(ITekConfContext context, ILog log) : base(context, log) { }

		public ActionResult Index()
		{
			return View();
		}
	}
}
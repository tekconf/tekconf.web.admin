using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;

namespace TekConf.Web.Controllers
{
	public class ControllerBase : Controller
	{
		private readonly ITekConfContext _context;
		private readonly ILog _log;

		public ControllerBase(ITekConfContext context, ILog log)
		{
			_context = context;
			_log = log;
		}

		protected ITekConfContext Context
		{
			get
			{
				return _context;
			}
		}

		protected ILog Log
		{
			get
			{
				return _log;
			}
		}
	}
}
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;
using TekConf.Data.Models;
using TekConf.Web.Admin.ViewModels.Conference;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace TekConf.Web.Admin.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ITekConfContext _context;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        public ConferenceController(ITekConfContext context, UserManager<User> userManager, ILog log)
        {
            _context = context;
            _userManager = userManager;
            _log = log;
        }

        public async Task<ActionResult> Index()
        {
            var conferences = await _context.Conferences.AsNoTracking().ToListAsync();
            return View(conferences);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new ConferenceCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConferenceCreateViewModel viewModel)
        {
            var conference = Mapper.Map<Conference>(viewModel.Conference);
            if (conference != null)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                conference.AddAdminUser(user);
                _context.Conferences.Add(conference);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
	}
}
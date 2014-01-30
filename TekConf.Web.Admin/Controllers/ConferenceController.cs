using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Logging;
using TekConf.Data;
using TekConf.Data.Models;
using TekConf.Web.Admin.Code;
using TekConf.Web.Admin.ViewModels.Conference;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace TekConf.Web.Admin.Controllers
{
    [Authorize]
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
            var image = Request.Form["ImageFile"];
            var conference = Mapper.Map<Conference>(viewModel.Conference);
            if (conference != null)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                conference.AddAdminUser(user);

                if (viewModel.ImageFile != null)
                {
                    IImageSaver imageSaver = null;

#if DEBUG
                    //TODO, Move this to configuration
                    imageSaver = new FileSystemImageSaver();
#else
				IImageSaverConfiguration configuration = new ImageSaverConfiguration();
				imageSaver = new AzureImageSaver(configuration);
#endif

                    string url = imageSaver.SaveImage(conference.Name.GenerateSlug() + Path.GetExtension(viewModel.ImageFile.FileName), viewModel.ImageFile);
                    conference.ImageUrl = url;
                }


                _context.Conferences.Add(conference);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
	}
}
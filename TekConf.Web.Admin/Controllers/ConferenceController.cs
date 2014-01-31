using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web;
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
			var conference = Mapper.Map<Conference>(viewModel.Conference);
			if (conference != null)
			{
				var user = _userManager.FindById(User.Identity.GetUserId());
				conference.AddAdminUser(user);

				var url = SaveUploadedImage(viewModel.ImageFile, conference);
				conference.ImageUrl = url;

				_context.Conferences.Add(conference);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

		private string SaveUploadedImage(HttpPostedFileBase imageFile, Conference conference)
		{
			string url = null;
			if (imageFile != null)
			{
				IImageSaver imageSaver = null;

#if DEBUG
				//TODO, Move this to configuration
				imageSaver = new FileSystemImageSaver();
#else
				IImageSaverConfiguration configuration = new ImageSaverConfiguration();
				imageSaver = new AzureImageSaver(configuration);
#endif

				url = imageSaver.SaveImage(conference.Name.GenerateSlug() + Path.GetExtension(imageFile.FileName), imageFile);
			}

			return url;
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int id)
		{
			var conference = await _context.Conferences.SingleOrDefaultAsync(c => c.Id == id);

			var dto = Mapper.Map<EditConferenceDto>(conference);

			var viewModel = new ConferenceEditViewModel { Conference = dto };
			return View(viewModel);

		}

		[HttpPost]
		public async Task<ActionResult> Edit(ConferenceEditViewModel viewModel)
		{
			var conference = await _context.Conferences.SingleOrDefaultAsync(c => c.Id == viewModel.Conference.Id);
			conference.CallForSpeakersCloses = viewModel.Conference.CallForSpeakersCloses;
			conference.CallForSpeakersOpens = viewModel.Conference.CallForSpeakersOpens;
			conference.DefaultTalkLength = viewModel.Conference.DefaultTalkLength;
			conference.Description = viewModel.Conference.Description;
			conference.End = viewModel.Conference.End;

			conference.IsLive = viewModel.Conference.IsLive;
			conference.IsOnline = viewModel.Conference.IsOnline;
			//conference.Location = viewModel.Conference.Location;
			conference.Name = viewModel.Conference.Name;
			//conference.PublishedDate = viewModel.Conference.PublishedDate;
			conference.RegistrationCloses = viewModel.Conference.RegistrationCloses;
			conference.RegistrationOpens = viewModel.Conference.RegistrationOpens;
			conference.Start = viewModel.Conference.Start;
			conference.Tagline = viewModel.Conference.Tagline;
			conference.WillProvideVideos = viewModel.Conference.WillProvideVideos;

			//var url = SaveUploadedImage(viewModel.ImageFile, conference);
			//conference.ImageUrl = url;

			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
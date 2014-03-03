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
using AutoMapper.QueryableExtensions;

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
			var dtos = await _context.Conferences
																.AsNoTracking()
																.Project().To<ConferenceDto>()
																.ToListAsync();

			var viewModel = new ConferenceIndexViewModel
			{
				Conferences = dtos
			};
			return View(viewModel);
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
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			var conference = Mapper.Map<Conference>(viewModel.Conference);
			if (conference != null)
			{
				var user = _userManager.FindById(User.Identity.GetUserId());
				conference.AddAdminUser(user);

				var url = SaveImage(viewModel.ImageFile, conference);
				conference.ImageUrl = url;

				var squareImageUrl = SaveSquareImage(viewModel.ImageFile, conference);
				conference.SquareImageUrl = squareImageUrl;

				_context.Conferences.Add(conference);
				await _context.SaveChangesAsync();
			}
			if (conference != null)
			{
				return RedirectToAction("Created", new { id = conference.Id });
			}
			else
			{
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public async Task<ActionResult> Created(int id)
		{
			var dto = await _context.Conferences.AsNoTracking()
									.Project().To<ConferenceCreatedDto>()
									.SingleOrDefaultAsync(c => c.Id == id);

			var viewModel = new ConferenceCreatedViewModel() { Conference = dto };

			return View(viewModel);
		}

		[HttpGet]
		public async Task<ActionResult> Detail(int id)
		{
			var dto = await _context.Conferences.Include(c => c.Sessions).AsNoTracking()
									.Project().To<ConferenceDetailDto>()
									.SingleOrDefaultAsync(c => c.Id == id);

			var viewModel = new ConferenceDetailViewModel { Conference = dto };

			return View(viewModel);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int id)
		{
			var dto = await _context.Conferences.AsNoTracking()
															.Project().To<EditConferenceDto>()
															.SingleOrDefaultAsync(c => c.Id == id);

			var viewModel = new ConferenceEditViewModel(HttpContext.Request.RequestContext.RouteData) { Conference = dto };
			return View(viewModel);

		}

		[HttpPost, LockForm]
		public async Task<ActionResult> Edit(ConferenceEditViewModel viewModel)
		{
			viewModel.SetRouteData(HttpContext.Request.RequestContext.RouteData);
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

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

			if (viewModel.ImageFile != null)
			{
				var url = SaveImage(viewModel.ImageFile, conference);
				if (!string.IsNullOrWhiteSpace(url))
				{
					conference.ImageUrl = url;
				}

				var squareImageUrl = SaveSquareImage(viewModel.ImageFile, conference);
				if (!string.IsNullOrWhiteSpace(squareImageUrl))
				{
					conference.SquareImageUrl = squareImageUrl;
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private string SaveImage(HttpPostedFileBase imageFile, Conference conference)
		{
			var imageName = conference.Name.GenerateSlug() + Path.GetExtension(imageFile.FileName);
			return SaveUploadedImage(imageName, imageFile, conference);
		}

		private string SaveSquareImage(HttpPostedFileBase imageFile, Conference conference)
		{
			var imageName = conference.Name.GenerateSlug() + "-sq" + Path.GetExtension(imageFile.FileName);
			return SaveUploadedImage(imageName, imageFile, conference);
		}

		private string SaveUploadedImage(string imageName, HttpPostedFileBase imageFile, Conference conference)
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
				url = imageSaver.SaveImage(imageName, imageFile);
			}

			return url;
		}
	}


}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Logging;
using Microsoft.AspNet.Identity;
using TekConf.Data;
using TekConf.Data.Models;
using TekConf.Web.Admin.Code;
using TekConf.Web.Admin.ViewModels.Session;

namespace TekConf.Web.Admin.Controllers
{
    public class SessionController : Controller
    {
		private readonly ITekConfContext _context;
		private readonly ILog _log;
		private readonly UserManager<User> _userManager;
        public SessionController(ITekConfContext context, UserManager<User> userManager, ILog log)
		{
			_context = context;
			_userManager = userManager;
			_log = log;
		}
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create(int conferenceId)
        {
            var conference = await _context.Conferences.Select(c => new { c.Id, c.Name }).SingleOrDefaultAsync(c => c.Id == conferenceId);
            var viewModel = new SessionCreateViewModel
            {
                ConferenceName = conference.Name, 
                ConferenceId = conference.Id
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SessionCreateViewModel viewModel)
        {
            var conference = await _context.Conferences.SingleOrDefaultAsync(c => c.Id == viewModel.ConferenceId);

            if (conference.Sessions.Any(s => s.Title == viewModel.Session.Title))
            {
                //TODO
            }
            else
            {
                var session = new Session
                {
                    Conference = conference,
                    Description = viewModel.Session.Description,
                    End = viewModel.Session.End,
                    Slug  = viewModel.Session.Title.GenerateSlug(),
                    Start = viewModel.Session.Start,
                    TalkLength  = viewModel.Session.TalkLength,
                    Title = viewModel.Session.Title,
                    WillProvideVideo = viewModel.Session.WillProvideVideo
                };
                conference.Sessions.Add(session);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Conference");
        }
	}
}
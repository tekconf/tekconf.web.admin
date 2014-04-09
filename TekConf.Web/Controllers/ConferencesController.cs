using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Common.Logging;
using EntityFramework.Extensions;
using TekConf.Data;
using TekConf.Web.ViewModels;

namespace TekConf.Web.Controllers
{
	public class ConferencesController : ControllerBase
	{
		public ConferencesController(ITekConfContext context, ILog log) : base(context, log) { }

		public async Task<ActionResult> Index()
		{
			ConferencesIndexViewModel viewModel = null;
			var endDate = DateTime.Now.Date.AddDays(-4);

			var conferences = this.Context
								.Conferences
								.Where(x => x.IsLive)
								.Where(x => x.End >= endDate)
								.OrderBy(x => x.Start)
								.Project().To<ConferencesIndexConferenceDto>()
								.Future();

			var totalCount = this.Context
				.Conferences
				.Where(x => x.IsLive)
				.Where(x => x.End >= endDate)
				.FutureCount();

			await Task.Run(() =>
			{
				viewModel = new ConferencesIndexViewModel
				{
					Conferences = conferences.ToList(),
					Filter = new ConferencesFilter(),
					ShowTable = false,
					TotalCount = totalCount.Value,
				};
			});

			return View(viewModel);
		}

		//[CompressFilter]
		public async Task<ActionResult> Detail(string slug)
		{
			if (string.IsNullOrWhiteSpace(slug))
				return RedirectToAction("Index");

			string userName = string.Empty;
			if (Request.IsAuthenticated)
			{
				userName = System.Web.HttpContext.Current.User.Identity.Name;
			}

			var conference = this.Context
														.Conferences
														.Where(x => x.Slug == slug)
														.Project().To<ConferenceDetailDto>()
														.FutureFirstOrDefault();

			ConferenceDetailViewModel viewModel = null;
			await Task.Run(() =>
			{
				viewModel = new ConferenceDetailViewModel
				{
					Conference = conference.Value
				};
			});

			return View(viewModel);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Logging;
using EntityFramework.Extensions;
using Newtonsoft.Json;
using TekConf.Data;
using TekConf.Data.Models;
using TekConf.Web.Temp;
using TekConf.Web.ViewModels;

namespace TekConf.Web.Controllers
{
	public class HomeController : ControllerBase
	{
		public HomeController(ITekConfContext context, ILog log) : base(context, log) { }

		public async Task<ActionResult> Import()
		{
			var client = new HttpClient();
			var json = await client.GetStringAsync("http://api.tekconf.com/v1/conferences?format=json");
			var conferences = JsonConvert.DeserializeObject<List<FullConferenceDto>>(json);
			var models = Mapper.Map<List<Conference>>(conferences);
			foreach (var model in models)
			{
				model.Start = DateTime.Now.AddDays(-10);
				model.End = DateTime.Now.AddMonths(2);
				model.CallForSpeakersOpens = DateTime.Now.AddDays(-10);
				model.RegistrationOpens = DateTime.Now.AddDays(-10);
				model.CallForSpeakersCloses = DateTime.Now.AddMonths(2);
				model.RegistrationCloses = DateTime.Now.AddMonths(2);

				this.Context.Conferences.Add(model);
			}
			await this.Context.SaveChangesAsync();
			return null;
		}

		public async Task<ActionResult> Index()
		{
			var futureDate = DateTime.Now.AddDays(-4);
			var conferences = this.Context.Conferences
				.OrderBy(x => x.Start)
				.Where(x => x.End >= futureDate)
				.Skip(0).Take(4)
				.Project().To<HomePageConferenceDto>()
				.Future();

			var totalCount = this.Context.Conferences
				.Where(x => x.End >= futureDate)
				.Future();

			HomePageViewModel viewModel = null;

			await Task.Run(() =>
			{
				viewModel = new HomePageViewModel
				{
					FeaturedConferences = conferences.ToList(),
					TotalCount = totalCount.Count()
				};
			});

			return View(viewModel);
		}
	}
}

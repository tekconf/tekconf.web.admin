using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TekConf.Web.ViewModels
{
	public class HomePageViewModel
	{
		public HomePageViewModel()
		{
			this.FeaturedConferences = Enumerable.Empty<HomePageConferenceDto>().ToList();
		}
		public List<HomePageConferenceDto> FeaturedConferences { get; set; }
		public int TotalCount { get; set; }
	}

	public class HomePageConferenceDto
	{
		public string Slug { get; set; }

		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public DateTime? Start { get; set; }
		public string Description { get; set; }
	}
}
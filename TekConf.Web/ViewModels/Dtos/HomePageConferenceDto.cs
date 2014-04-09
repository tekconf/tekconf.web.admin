using System;

namespace TekConf.Web.ViewModels
{
	public class HomePageConferenceDto
	{
		public string Slug { get; set; }

		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public DateTime? Start { get; set; }
		public string Description { get; set; }
	}
}
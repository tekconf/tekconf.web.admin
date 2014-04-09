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
}
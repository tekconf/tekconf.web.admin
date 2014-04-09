using System.Collections.Generic;
using System.Linq;

namespace TekConf.Web.ViewModels
{
	public class ConferencesIndexViewModel
	{
		public ConferencesIndexViewModel()
		{
			this.Conferences = Enumerable.Empty<ConferencesIndexConferenceDto>().ToList();
		}

		public List<ConferencesIndexConferenceDto> Conferences { get; set; }
		public int TotalCount { get; set; }
		public bool ShowTable { get; set; }
		public ConferencesFilter Filter { get; set; }
	}
}
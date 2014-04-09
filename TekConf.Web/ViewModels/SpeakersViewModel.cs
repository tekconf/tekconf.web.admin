using System.Collections.Generic;
using System.Linq;

namespace TekConf.Web.ViewModels
{
	public class SpeakersViewModel
	{
		public SpeakersViewModel()
		{
			this.OpenConferences = Enumerable.Empty<SpeakersConferenceDto>().ToList();
			this.Presentations = Enumerable.Empty<PresentationDto>().ToList();
			this.MyConferences = Enumerable.Empty<SpeakersConferenceDto>().ToList();
		}
		public List<SpeakersConferenceDto> OpenConferences { get; set; }
		public List<PresentationDto> Presentations { get; set; }
		public List<SpeakersConferenceDto> MyConferences { get; set; }
	}
}
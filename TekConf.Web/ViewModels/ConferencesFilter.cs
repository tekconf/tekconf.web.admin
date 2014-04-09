using System;

namespace TekConf.Web.ViewModels
{
	public class ConferencesFilter
	{
		public string SortBy { get; set; }
		public bool ShowPastConferences { get; set; }
		public bool ShowOnlyOpenCalls { get; set; }
		public bool ShowOnlyOnSale { get; set; }
		public string ViewAs { get; set; }
		public string Search { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public double? Distance { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }

	}
}
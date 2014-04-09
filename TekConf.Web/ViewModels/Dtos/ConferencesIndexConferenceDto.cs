using System;

namespace TekConf.Web.ViewModels
{
	public class ConferencesIndexConferenceDto
	{
		public string Slug { get; set; }

		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }
		public string Description { get; set; }
		public bool IsOnline { get; set; }
		public virtual LocationDto Location { get; set; }

		public string DateRange
		{
			get
			{

				string range;
				if (!Start.HasValue || !End.HasValue)
				{
					range = "No Date Set";
				}
				else if (Start.Value.Month == End.Value.Month && Start.Value.Year == End.Value.Year)
				{
					// They begin and end in the same month
					if (Start.Value.Date == End.Value.Date)
					{
						range = Start.Value.ToString("MMMM") + " " + Start.Value.Day + ", " + Start.Value.Year;
					}
					else
						range = Start.Value.ToString("MMMM") + " " + Start.Value.Day + " - " + End.Value.Day + ", " + Start.Value.Year;
				}
				else
				{
					// They begin and end in different months
					if (Start.Value.Year == End.Value.Year)
					{
						range = Start.Value.ToString("MMMM") + " " + Start.Value.Day + " - " + End.Value.ToString("MMMM") + " " + End.Value.Day + ", " + Start.Value.Year;
					}
					else
					{
						range = Start.Value.ToString("MMMM") + " " + Start.Value.Day + ", " + Start.Value.Year + " - " + End.Value.ToString("MMMM") + " " + End.Value.Day + ", " + End.Value.Year;
					}
				}

				return range;
			}
		}

		public string FormattedAddress
		{
			get
			{
				string formattedAddress;
				if (IsOnline == true)
				{
					formattedAddress = "online";
				}
				else if (Location == null)
				{
					formattedAddress = "No location set";
				}
				else if (!string.IsNullOrWhiteSpace(Location.City) && !string.IsNullOrWhiteSpace(Location.State))
				{
					formattedAddress = Location.City + ", " + Location.State;
				}
				else if (!string.IsNullOrWhiteSpace(Location.City) && !string.IsNullOrWhiteSpace(Location.Country))
				{
					formattedAddress = Location.City + ", " + Location.Country;
				}
				else if (!string.IsNullOrWhiteSpace(Location.City))
				{
					formattedAddress = Location.City;
				}
				else
				{
					formattedAddress = "No location set";
				}

				return formattedAddress;
			}
		}
	}
}
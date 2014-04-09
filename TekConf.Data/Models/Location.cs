using System.Collections.Generic;

namespace TekConf.Data.Models
{
	public class Location
	{
		public int Id { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public int? StreetNumber { get; set; }
		public string BuildingName { get; set; }
		public string StreetNumberSuffix { get; set; }
		public string StreetName { get; set; }
		public string StreetType { get; set; }
		public string StreetDirection { get; set; }
		public string AddressType { get; set; }
		public string AddressTypeId { get; set; }
		public string LocalMunicipality { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string GoverningDistrict { get; set; }
		public string PostalArea { get; set; }
		public string Country { get; set; }
		public virtual ICollection<Room> Rooms { get; set; }
		public virtual ICollection<Conference> Conferences { get; set; }

	}
}
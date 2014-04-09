using AutoMapper;
using TekConf.Data.Models;
using TekConf.Web.Temp;
using TekConf.Web.ViewModels;

namespace TekConf.Web.Code
{
	public class AutoMapperBootstrapper
	{
		public static void Init()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile<ConferenceProfile>();
				cfg.AddProfile<OldProfile>();
			});
		}
	}

	public class OldProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<FullConferenceDto, Conference>()
				.ForMember(x => x.Location, opt => opt.Ignore());
			Mapper.CreateMap<FullSessionDto, Session>();
			//Mapper.CreateMap<FullSpeakerDto, Speaker>();
			Mapper.CreateMap<AddressDto, Location>();
		}

		public override string ProfileName
		{
			get { return this.GetType().Name; }
		}
	}

	public class ConferenceProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<Conference, HomePageConferenceDto>();
			
			Mapper.CreateMap<Conference, ConferencesIndexConferenceDto>()
				.ForMember(x => x.Location, opt => opt.Ignore()); 
			
			Mapper.CreateMap<Location, LocationDto>()
				.ForMember(x => x.Rooms, opt => opt.Ignore());
			
			Mapper.CreateMap<Room, RoomDto>();
			
			Mapper.CreateMap<Conference, ConferenceDetailDto>()
				.ForMember(x => x.Location, opt => opt.Ignore());

			Mapper.CreateMap<Conference, SpeakersConferenceDto>()
				.ForMember(x => x.Location, opt => opt.Ignore());
		}

		public override string ProfileName
		{
			get { return this.GetType().Name; }
		}
	}
}
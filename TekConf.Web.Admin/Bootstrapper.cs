﻿using AutoMapper;
using TekConf.Data.Models;
using TekConf.Web.Admin.ViewModels.Conference;

namespace TekConf.Web.Admin
{
	public class Bootstrapper
	{
		public Bootstrapper()
		{

		}

		public void Bootstrap()
		{
			Mapper.CreateMap<CreateConferenceDto, Conference>();
			Mapper.CreateMap<Conference, EditConferenceDto>();
			Mapper.CreateMap<EditConferenceDto, Conference>();
			Mapper.CreateMap<Conference, Conference>()
				.ForMember(x => x.Id, opt => opt.Ignore());

		}
	}
}
using AutoMapper;
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
        }
    }
}
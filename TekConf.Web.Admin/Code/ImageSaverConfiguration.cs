using System.Configuration;

namespace TekConf.Web.Admin.Code
{
    public class ImageSaverConfiguration : IImageSaverConfiguration
    {
        public string ImageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageUrl"];
            }
        }

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;
            }
        }
    }
}
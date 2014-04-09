using System.Web;
using System.Web.Mvc;

namespace TekConf.Web
{
	public static class HtmlHelpers
	{
		public static string SocialIcon(this HtmlHelper helper, string provider)
		{
			if (provider == "google")
				return "fa-google-plus";

			if (provider == "microsoft")
				return "fa-windows";

			if (provider == "yahoo")
				return "icon-yahoo";

			return "fa-" + provider;
		}


	}
}
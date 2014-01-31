using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace TekConf.Web.Admin.Code
{
    public class ViewModelBase
    {
        public ViewModelBase() { }

        [HiddenInput(DisplayValue = false)]
        public string FormName { get; set; }

        public ViewModelBase(RouteData routeData)
        {
            SetRouteData(routeData);
        }

        public void SetRouteData(RouteData routeData)
        {
            FormName = routeData.Values.Select(v => v.Value.ToString()).Aggregate((current, next) => current + "-" + next);
        }
    }
}
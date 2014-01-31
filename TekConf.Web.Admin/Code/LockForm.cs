using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace TekConf.Web.Admin.Code
{
    public class LockForm : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var viewModel = filterContext.ActionParameters.FirstOrDefault(x => x.Key.ToLower().Contains("model")).Value as ViewModelBase;

            if (viewModel == null)
            {
                return;
            }

            SendLockFormSignalRMessage(viewModel);
        }

        private void SendLockFormSignalRMessage(ViewModelBase viewModel)
        {
            viewModel.SetRouteData(HttpContext.Current.Request.RequestContext.RouteData);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FormEditingHub>();

            hubContext.Clients.Group(viewModel.FormName).disableForm(new FormDisabledMessage()
            {
                FormName = viewModel.FormName,
                SavedAt = DateTime.Now
            }
                                                                    );
        }
    }
}
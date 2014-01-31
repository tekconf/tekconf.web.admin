using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TekConf.Web.Admin
{
    public class FormDisabledMessage
    {
        public string FormName { get; set; }
        public string UserName { get; set; }
        public DateTime SavedAt { get; set; }
    }
    public class FormEditingHub : Hub
    {
        public void FormUpdated(string form, string userName)
        {
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}
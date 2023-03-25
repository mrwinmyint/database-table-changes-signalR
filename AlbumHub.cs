using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMvcApp
{
    public class AlbumHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlbumHub>();
            context.Clients.All.displayStatus();
        }
    }
}
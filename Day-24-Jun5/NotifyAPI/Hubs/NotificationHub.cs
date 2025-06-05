using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace NotifyAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "staff")
                await Groups.AddToGroupAsync(Context.ConnectionId, "StaffGroup");
            await base.OnConnectedAsync();
        }
    }
}
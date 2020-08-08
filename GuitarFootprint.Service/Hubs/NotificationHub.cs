using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Service.Abstraction.Manager;
using Microsoft.AspNetCore.SignalR;

namespace GuitarFootprint.Service.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IConnectionManager _connectionManager;

        public NotificationHub(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public Task<string> GetConnectionId()
        {
            _connectionManager.AddConnection(Guid.Empty, Context.ConnectionId);

            return Task.FromResult(Context.ConnectionId);
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                _connectionManager.AddConnection(Guid.Empty, Context.ConnectionId);
            }
            catch (Exception e)
            {
                // ignored
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _connectionManager.RemoveConnection(Context.ConnectionId);
            }
            catch (Exception e)
            {
                // ignored
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarFootprint.Service.Abstraction.Services
{
    public interface IHubNotificationService
    {
        Task SendNotificationToAll<TDto>(string method, TDto messageDto);
        IEnumerable<Guid> GetOnlineUsers();
        Task SendNotificationAsync<TDto>(Guid userId, string method, TDto messageDto);
    }
}

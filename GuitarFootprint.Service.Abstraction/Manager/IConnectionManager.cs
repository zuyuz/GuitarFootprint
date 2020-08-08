using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarFootprint.Service.Abstraction.Manager
{
    public interface IConnectionManager
    {
        IEnumerable<Guid> OnlineUsers { get; }
        Task AddConnection(Guid userId, string connectionId);
        Task RemoveConnection(string connectionId);
        Task<HashSet<string>> GetConnection(Guid userId);
    }
}

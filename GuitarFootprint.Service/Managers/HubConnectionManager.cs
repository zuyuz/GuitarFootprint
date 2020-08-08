using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Service.Abstraction.Manager;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Managers
{
    public class HubConnectionManager : IConnectionManager
    {
        private static readonly Dictionary<Guid, System.Collections.Generic.HashSet<string>> userMap = new Dictionary<Guid, System.Collections.Generic.HashSet<string>>();
        public IEnumerable<Guid> OnlineUsers => userMap.Keys;

        public Task AddConnection(Guid userId, string connectionId)
        {
            return Try(() =>
            {
                lock (userMap)
                {
                    if (!userMap.ContainsKey(userId))
                    {
                        userMap[userId] = new System.Collections.Generic.HashSet<string>();
                    }

                    userMap[userId].Add(connectionId);
                }

                return unit;
            }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }

        public Task RemoveConnection(string connectionId)
        {
            return Try(() =>
            {
                lock (userMap)
                {
                    foreach (var userId in userMap.Keys.Where(userId => userMap.ContainsKey(userId)))
                    {
                        userMap[userId].Remove(connectionId);
                        break;
                    }
                }
                return unit;
            }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }

        public Task<System.Collections.Generic.HashSet<string>> GetConnection(Guid userId)
        {
            return Try(() =>
            {
                lock (userMap)
                {
                    return userMap[userId];
                }
            }).Match(hashSet => hashSet.AsTask(), exception => exception.AsFailedTask<System.Collections.Generic.HashSet<string>>());
        }
    }
}

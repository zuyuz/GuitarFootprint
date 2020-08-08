using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Service.Abstraction.Manager;
using GuitarFootprint.Service.Abstraction.Services;
using GuitarFootprint.Service.Hubs;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Services
{
    public class HubNotificationService : IHubNotificationService
    {
        private readonly IHubContext<NotificationHub> _context;
        private readonly IConnectionManager _connectionManager;

        public HubNotificationService(IHubContext<NotificationHub> context, IConnectionManager connectionManager)
        {
            _context = context;
            _connectionManager = connectionManager;
        }

        public Task SendNotificationToAll<TDto>(string method, TDto messageDto)
        {
            return TryAsync(async () =>
            {
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var json = JsonConvert.SerializeObject(messageDto, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                await _context.Clients.All.SendAsync(method, json);
                return unit;
            }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }

        public IEnumerable<Guid> GetOnlineUsers()
        {
            return _connectionManager.OnlineUsers;
        }

        public Task SendNotificationAsync<TDto>(Guid userId, string method, TDto messageDto)
        {
            return TryAsync(_connectionManager.GetConnection(userId))
                .Filter(set => set != null && set.Count > 0)
                .Do(set =>
                {
                    Try(() =>
                    {
                        return set.Select(async conn =>
                        {
                            var contractResolver = new DefaultContractResolver
                            {
                                NamingStrategy = new CamelCaseNamingStrategy()
                            };

                            var json = JsonConvert.SerializeObject(messageDto, new JsonSerializerSettings
                            {
                                ContractResolver = contractResolver,
                                Formatting = Formatting.Indented
                            });
                            await _context.Clients.Clients(conn).SendAsync(method, json);
                            return unit;
                        }).ToList();
                    });
                }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }
    }
}

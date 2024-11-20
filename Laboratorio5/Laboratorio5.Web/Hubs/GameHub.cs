using Laboratorio5.Web.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace Laboratorio5.Web.Hubs
{
    // ES2: Hub
    public interface IGameClientEvent
    {
        public System.Threading.Tasks.Task NuovoMessaggio(Guid idUtente, string email, string messaggio);
        public System.Threading.Tasks.Task AzioneEseguita(Guid idUtente, string email, int idAzione);
    }

    [Microsoft.AspNetCore.Authorization.Authorize]
    public class GameHub : Hub<IGameClientEvent>
    {
        private readonly IPublishDomainEvents _publisher;

        public GameHub(IPublishDomainEvents publisher)
        {
            _publisher = publisher;
        }

        public async System.Threading.Tasks.Task EntraNelGruppo(Guid idPartita)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idPartita.ToString());
        }
        public async System.Threading.Tasks.Task EsciDalGruppo(Guid idPartita)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idPartita.ToString());
        }
    }
}

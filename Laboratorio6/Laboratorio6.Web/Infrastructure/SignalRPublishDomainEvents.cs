using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using Laboratorio6.Web.Hubs;
using Laboratorio6.Web.Areas.Game.Game;

namespace Laboratorio6.Web.Infrastructure
{
    // ES3: Publisher
    public class SignalrPublishDomainEvents : IPublishDomainEvents
    {
        IHubContext<GameHub, IGameClientEvent> _gameHub;

        public SignalrPublishDomainEvents(
            IHubContext<GameHub, IGameClientEvent> gameHub)
        {
            _gameHub = gameHub;
        }

        private IGameClientEvent GetGameGroup(Guid idPartita)
        {
            return _gameHub.Clients.Group(idPartita.ToString());
        }

        public Task Publish(object evnt)
        {
            try
            {
                return ((dynamic)this).When((dynamic)evnt);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return Task.CompletedTask;
            }
        }

        public Task When(NuovoMessaggioEvent e)
        {
            return GetGameGroup(e.IdPartita).NuovoMessaggio(e.IdUtente, e.Email, e.Messaggio);
        }

        public Task When(AzioneEseguitaEvent e)
        {
            return GetGameGroup(e.IdPartita).AzioneEseguita(e.IdUtente, e.Email, e.IdAzione);
        }
    }
}

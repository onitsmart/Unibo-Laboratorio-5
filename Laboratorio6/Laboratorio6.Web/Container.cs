using Laboratorio6.Services.Utenti;
using Laboratorio6.Web.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Laboratorio6.Web
{
    public class Container
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            // ES2 Registrazione servizio per Utenti
            container.AddScoped<UtentiService>();

            // ES3: Registrazione publisher
            container.AddScoped<IPublishDomainEvents, Infrastructure.SignalrPublishDomainEvents>();
        }
    }
}

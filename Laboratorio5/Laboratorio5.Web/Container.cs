using Laboratorio5.Services.Utenti;
using Laboratorio5.Web.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Laboratorio5.Web
{
    public class Container
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            // ES3: Registrazione servizio per Utenti
            container.AddScoped<UtentiService>();

            // ES2: Registrazione publisher
            container.AddScoped<IPublishDomainEvents, Infrastructure.SignalrPublishDomainEvents>();
        }
    }
}

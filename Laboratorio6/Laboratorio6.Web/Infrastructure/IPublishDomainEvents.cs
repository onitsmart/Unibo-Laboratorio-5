using System.Threading.Tasks;

namespace Laboratorio6.Web.Infrastructure
{
    public interface IPublishDomainEvents
    {
        Task Publish(object evnt);
    }
}

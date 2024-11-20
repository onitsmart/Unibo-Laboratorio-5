using System.Threading.Tasks;

namespace Laboratorio5.Web.Infrastructure
{
    public interface IPublishDomainEvents
    {
        Task Publish(object evnt);
    }
}

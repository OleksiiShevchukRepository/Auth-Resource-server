using Core.Entities;
using Core.Interfaces;

namespace Services
{
    internal class EventService : MongoDataService<Event, IRepository<Event>>, IEventService
    {
        public EventService(IWebApplicationConfig config) : base(config)
        {
        }

        protected override void Initialize(out IRepository<Event> repository)
        {
            repository = Context.Events;
        }
    }
}
using System;
using Core.Entities;
using Core.Interfaces;

namespace Services
{
    internal class EventService : SqlDataService<Event, IRepository<Event>>, IEventService
    {
        public EventService(IWebApplicationConfig config) : base(config)
        {
        }

        protected override void Initialize(out IRepository<Event> repository)
        {
            throw new NotImplementedException();
        }
    }
}
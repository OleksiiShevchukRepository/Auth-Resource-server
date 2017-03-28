using System;
using System.Web.Http;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;

namespace ResourceServer.Controllers
{
    [Authorize]
    public class EventController : DataApiController<Event, IEventService>
    {
        public EventController(IEventService service) : base(service)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddNewEvent")]
        public IHttpActionResult AddNewEvent()
        {
            try
            {
                var newEvent = new Event
                {
                    Name = "Movie Logan",
                    Description = "Logan Description",
                    EventType = Enums.EventType.Cinema,
                    StartDate = DateTime.UtcNow,
                    ImageId = null
                };
                Service.Add(newEvent);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
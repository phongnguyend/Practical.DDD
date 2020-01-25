using System;
using System.Linq;
using ClassifiedAds.DomainServices.Entities;
using ClassifiedAds.DomainServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassifiedAds.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger _logger;

        public EventsController(IEventService eventService, ILogger<EventsController> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_eventService.Get());
        }


        [HttpGet("{id}")]
        public IActionResult Get(Guid Id)
        {
            return Ok(_eventService.GetEventIncludeSessions(Id));
        }

        [HttpPost("")]
        public IActionResult Post(Event model)
        {
            _eventService.Add(model);
            return Ok(model);
        }

        [HttpPost("{eventId}/sessions")]
        public IActionResult CreateSession(Guid eventId, Session model)
        {
            var @event = _eventService.GetEventIncludeSessions(eventId);
            @event.Sessions.Add(model);
            _eventService.Update(@event);
            return Ok(model);
        }

        [HttpPost("{eventId}/sessions/{sessionId}/voters/{voterId}")]
        public IActionResult Vote(Guid eventId, Guid sessionId, Guid voterId)
        {
            var @event = _eventService.GetEventIncludeSessions(eventId);
            var session = @event.Sessions.FirstOrDefault(x => x.Id == sessionId);
            session.Voters.Add(new Voter { UserId = voterId });
            _eventService.Update(@event);
            return Ok(@event);
        }

        [HttpDelete("{eventId}/sessions/{sessionId}/voters/{voterId}")]
        public IActionResult UnVote(Guid eventId, Guid sessionId, Guid voterId)
        {
            var @event = _eventService.GetEventIncludeSessions(eventId);
            var session = @event.Sessions.FirstOrDefault(x => x.Id == sessionId);
            var vote = session.Voters.FirstOrDefault(x => x.UserId == voterId);
            session.Voters.Remove(vote);
            _eventService.Update(@event);
            return Ok(@event);
        }
    }
}
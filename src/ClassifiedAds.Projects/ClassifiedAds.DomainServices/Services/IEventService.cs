using ClassifiedAds.DomainServices.Entities;
using System;

namespace ClassifiedAds.DomainServices.Services
{
    public interface IEventService : IGenericService<Event>
    {
        Event GetEventIncludeSessions(Guid Id);
    }
}

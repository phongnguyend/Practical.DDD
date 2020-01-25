using ClassifiedAds.DomainServices.Entities;
using System.Collections.Generic;

namespace ClassifiedAds.DomainServices.Services
{
    public interface ISessionService : IGenericService<Session>
    {
        IList<Session> SearchSessions(string name);
    }
}

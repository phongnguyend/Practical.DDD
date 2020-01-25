using System.Collections.Generic;
using System.Linq;
using ClassifiedAds.DomainServices.Entities;
using ClassifiedAds.DomainServices.Repositories;

namespace ClassifiedAds.DomainServices.Services
{
    public class SessionService : GenericService<Session>, ISessionService
    {
        public SessionService(IUnitOfWork unitOfWork, IRepository<Session> sessionRepository) : base(unitOfWork, sessionRepository)
        {

        }

        public IList<Session> SearchSessions(string name)
        {
            return _repository.GetAll().Where(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).ToList();
        }
    }
}

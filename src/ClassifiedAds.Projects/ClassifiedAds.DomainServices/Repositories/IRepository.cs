using System.Linq;

namespace ClassifiedAds.DomainServices
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);
    }
}

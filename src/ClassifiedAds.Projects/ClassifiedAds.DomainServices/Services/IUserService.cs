using ClassifiedAds.DomainServices.Entities;
using System;

namespace ClassifiedAds.DomainServices
{
    public interface IUserService
    {
        User GetUserById(Guid Id);
    }
}

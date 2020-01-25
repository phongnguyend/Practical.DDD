using ClassifiedAds.DomainServices.Entities;
using System;
using System.Collections.Generic;

namespace ClassifiedAds.DomainServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();

        Product GetById(Guid Id);

        Product Create(Product product);

        Product Update(Product product);

        void Delete(Product product);
    }
}

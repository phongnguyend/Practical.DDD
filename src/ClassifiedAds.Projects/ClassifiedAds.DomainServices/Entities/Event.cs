using System;
using System.Collections.Generic;

namespace ClassifiedAds.DomainServices.Entities
{
    public class Event : Entity<Guid>
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public Location Location { get; set; }

        public string OnlineUrl { get; set; }

        public IList<Session> Sessions { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}

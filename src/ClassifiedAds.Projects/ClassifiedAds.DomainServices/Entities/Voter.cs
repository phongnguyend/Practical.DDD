using System;

namespace ClassifiedAds.DomainServices.Entities
{
    public class Voter : Entity<Guid>
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
    }
}

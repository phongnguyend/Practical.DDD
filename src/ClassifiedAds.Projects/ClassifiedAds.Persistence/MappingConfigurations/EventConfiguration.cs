using ClassifiedAds.DomainServices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassifiedAds.Persistence.MappingConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.OwnsOne(x => x.Location);
        }
    }
}

namespace ClassifiedAds.DomainServices.DomainEvents
{
    public interface IDomainEventHandler<T>
           where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}

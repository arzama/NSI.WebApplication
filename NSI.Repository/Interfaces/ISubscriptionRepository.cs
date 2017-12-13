using System;
using System.Collections.Generic;
using NSI.DC.SubscriptionRepository;

namespace NSI.Repository.Interfaces
{
    public interface ISubscriptionRepository
    {
        SubscriptionDto GetSubscription(int subscriptionId);
        IEnumerable<SubscriptionDto> GetAllSubscriptions();
        IEnumerable<SubscriptionDto> GetActiveSubscriptions();
        SubscriptionDto SaveSubscription(SubscriptionDto subscription);
        bool DeleteSubscription(int subscriptionId);
        SubscriptionDto GetCustomerSubscription(int customerId);

    }

}
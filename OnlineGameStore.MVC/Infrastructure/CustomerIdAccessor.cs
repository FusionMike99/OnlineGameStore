using System;
using OnlineGameStore.DomainModels;
using OnlineGameStore.DomainModels.Constants;

namespace OnlineGameStore.MVC.Infrastructure
{
    public class CustomerIdAccessor : ICustomerIdAccessor
    {
        public Guid GetCustomerId()
        {
            return Guid.Parse(Constants.DefaultCustomerId);
        }
    }
}
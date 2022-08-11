using System;

namespace OnlineGameStore.MVC.Infrastructure
{
    public interface ICustomerIdAccessor
    {
        Guid GetCustomerId();
    }
}
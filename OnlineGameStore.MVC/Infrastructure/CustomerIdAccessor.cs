using System;
using OnlineGameStore.BLL.Utils;

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
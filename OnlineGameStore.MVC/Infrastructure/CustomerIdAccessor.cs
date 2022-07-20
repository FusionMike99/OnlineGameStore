using System;
using Microsoft.AspNetCore.Http;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.MVC.Infrastructure
{
    public class CustomerIdAccessor : ICustomerIdAccessor
    {
        public string GetCustomerId()
        {
            return Constants.DefaultCustomerId;
        }
    }
}
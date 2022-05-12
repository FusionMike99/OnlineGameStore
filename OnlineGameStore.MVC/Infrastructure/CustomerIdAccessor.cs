using System;
using Microsoft.AspNetCore.Http;

namespace OnlineGameStore.MVC.Infrastructure
{
    public class CustomerIdAccessor : ICustomerIdAccessor
    {
        private readonly HttpContext _httpContext;

        public CustomerIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public int GetCustomerId()
        {
            var isCookieGet = _httpContext.Request.Cookies.TryGetValue("customerId", out var customerId);
            
            if (isCookieGet)
            {
                var isCustomerIdParsed = int.TryParse(customerId, out var parsingCustomerIdResult);

                if (isCustomerIdParsed)
                {
                    return parsingCustomerIdResult;
                }
            }

            var random = new Random();
            var randomCustomerId = random.Next();

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMonths(1)
            };

            _httpContext.Response.Cookies.Append("customerId", randomCustomerId.ToString(), cookieOptions);

            return randomCustomerId;
        }
    }
}
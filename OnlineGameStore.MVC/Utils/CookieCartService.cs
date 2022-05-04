using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Services;
using OnlineGameStore.MVC.Infrastructure;
using System;

namespace OnlineGameStore.MVC.Utils
{
    public class CookieCartService : CartService
    {
        [JsonIgnore]
        public HttpContext HttpContext { get; set; }

        [JsonIgnore]
        public CookieOptions Options { get; set; }

        public CookieCartService()
        {
            Options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMonths(1)
            };
        }

        public static CartService GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext;

            var cart = httpContext?.Request.Cookies.GetJson<CookieCartService>("Cart") ?? new CookieCartService();
            cart.HttpContext = httpContext;

            return cart;
        }

        public override void AddItem(Game product, short quantity)
        {
            base.AddItem(product, quantity);

            HttpContext?.Response.Cookies.SetJson("Cart", this, Options);
        }

        public override void RemoveItem(Game product)
        {
            base.RemoveItem(product);

            HttpContext?.Response.Cookies.SetJson("Cart", this, Options);
        }

        public override void Clear()
        {
            base.Clear();

            HttpContext?.Response.Cookies.Delete("Cart");
        }
    }
}

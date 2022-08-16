using Microsoft.Extensions.DependencyInjection;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public static class PaymentMethodsInjection
    {
        public static void AddPaymentMethods(this IServiceCollection services)
        {
            services.AddScoped<IPaymentMethodStrategy, BankPaymentMethodStrategy>();
            services.AddScoped<IPaymentMethodStrategy, TerminalIBoxPaymentMethodStrategy>();
            services.AddScoped<IPaymentMethodStrategy, VisaPaymentMethodStrategy>();
        }
    }
}
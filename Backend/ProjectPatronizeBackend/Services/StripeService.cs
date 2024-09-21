using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPatronizeBackend.Models;
using Stripe;

namespace ProjectPatronizeBackend.Services
{
    public class StripeService
    {
        public async Task<Stripe.Subscription> CreateStripeSubscription(string email, string planId)
        {
            var customerOptions = new CustomerCreateOptions { Email = email };
            var customerService = new CustomerService();
            var customer = await customerService.CreateAsync(customerOptions);

            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customer.Id,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions { Plan = planId }
                }
            };

            var subscriptionService = new Stripe.SubscriptionService();
            var subscription = await subscriptionService.CreateAsync(subscriptionOptions);

            return subscription;
        }
        public async Task<Customer> GetOrCreateCustomerAsync(string email)
        {
            var customerService = new CustomerService();

            // Sprawdź, czy klient już istnieje
            var customers = await customerService.ListAsync(new CustomerListOptions { Email = email });
            
            if (customers.Data.Any())
            {
                // Zwróć istniejącego klienta
                return customers.Data.First();
            }

            // Utwórz nowego klienta, jeśli nie istnieje
            var createOptions = new CustomerCreateOptions
            {
                Email = email
            };
            return await customerService.CreateAsync(createOptions);
        }

        public async Task<PaymentMethod> CreatePaymentMethodAsync(string cardToken)
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Token = cardToken, // Token płatności, np. `tok_visa` dla testowej karty
                },
            };
            var service = new PaymentMethodService();
            var paymentMethod = await service.CreateAsync(options);
            return paymentMethod;
        }
        public async Task AttachPaymentMethodToCustomerAsync(string customerId, string paymentMethodId)
        {
            var attachOptions = new PaymentMethodAttachOptions
            {
                Customer = customerId,
            };
            var paymentMethodService = new PaymentMethodService();
            await paymentMethodService.AttachAsync(paymentMethodId, attachOptions);
        }
    }
}
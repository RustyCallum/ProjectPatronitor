using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ProjectPatronizeBackend.API.User
{
    [ApiController]
    [Route("webhooks/stripe")]
    public class StripePaymentWebhook : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            // Odczytaj body żądania webhooka
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Weryfikacja webhooka przy użyciu sekretnych kluczy
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    "whsec_ccb4ba00fbb7223f3c1bddf0be1adae3a6714e69ef23bbd2f84cad5863c30a6b"  // Podaj swój klucz sekretu webhooka
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    // Przetwarzaj dane np. zapisuj email użytkownika do bazy danych
                    var customerEmail = session.CustomerDetails.Email;
                    // Inne przetwarzanie

                    // Zapisz dane do bazy danych lub inne akcje
                }

                return Ok();  // Zwróć 200 do Stripe, aby potwierdzić, że webhook został przetworzony
            }
            catch (StripeException e)
            {
                return BadRequest();  // Zwróć 400 jeśli coś poszło nie tak
            }
        }
    }
}
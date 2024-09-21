using Microsoft.AspNetCore.Mvc;
using ProjectPatronizeBackend.API.Payment;
using ProjectPatronizeBackend.DB;
using ProjectPatronizeBackend.Services;
using Stripe;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/asdf")]
    public class PaymentController : ControllerBase
    {
        private readonly StripeService _stripeService;
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
            _stripeService = new StripeService();
        }
        
        [HttpPost("create-checkout-session")]
        public async Task<ActionResult> CreateCheckoutSession(TierCheckoutRequest request)
        {
            var tier = await _context.Tiers.FindAsync(request.TierId);
            if (tier == null) return NotFound();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                    "paypal"
                    },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(tier.Price * 100),
                            Currency = "pln",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = tier.Name,
                                Description = tier.Description
                            },
                            Recurring = new SessionLineItemPriceDataRecurringOptions
                            {
                                Interval = "month",
                                IntervalCount = 1
                            }
                        },
                        Quantity = 1,
                        
                    }
                },
                Mode = "subscription",
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/cancel",

                BillingAddressCollection = "required"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return new JsonResult(new { url = session.Url });
        }
    }
}

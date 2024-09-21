using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatronizeBackend.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } // np. 'Succeeded', 'Pending', 'Failed'
        public string StripePaymentIntentId { get; set; } // Identyfikator płatności w Stripe.
    }
}
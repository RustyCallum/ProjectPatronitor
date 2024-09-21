using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectPatronizeBackend.DB;
using ProjectPatronizeBackend.Models;

namespace ProjectPatronizeBackend.Services
{
    public class SubscriptionService
    {
        private readonly StripeService _stripeService;
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext dbContext, StripeService stripeService)
        {
            _stripeService = stripeService;
            _context = dbContext;
        }
        
        //public async Task<Subscription> CreateSubscription(string email, string firstName, string lastName, Guid tierId)
        //{
        //    var tier = await _context.Tiers.FindAsync(tierId);
        //    if (tier == null) throw new Exception("Tier not found");

//            var stripeSubscription = await _stripeService.CreateStripeSubscription(email, tier.StripePlanId);
//
  //          var user = new User
    //        {
      //          Email = email,
        //        FirstName = firstName,
          //      LastName = lastName
            //};

            //var subscription = new Subscription
            //{
                //User = user,
                //Tier = tier,
                //StartDate = DateTime.UtcNow,
              //  StripeSubscriptionId = stripeSubscription.Id
            //};

            //_context.Subscriptions.Add(subscription);
            //await _context.SaveChangesAsync();

          //  return subscription;
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPatronizeBackend.API.Tier.Create;
using ProjectPatronizeBackend.DB;
using Stripe.Checkout;

namespace ProjectPatronizeBackend.API.Tier
{
    [ApiController]
    [Route("api/tier")]
    public class TierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TierController(AppDbContext context)
        {
            _context = context;
        }

        [HttpDelete]
        public async Task<IActionResult> GetSubscriptionTiers(Guid id)
        {
            await _context.Tiers.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok($"Tier with id {id} deleted succesfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionTiers()
        {
            var TierList = await _context.Tiers.ToListAsync();
            return Ok(TierList);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostSubscriptionTiers(TierCreateRequest req)
        {
            var newTier = new Models.Tier{
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
            };

            _context.Tiers.Add(newTier);
            await _context.SaveChangesAsync();
            return Ok(newTier);
        }
    }
}
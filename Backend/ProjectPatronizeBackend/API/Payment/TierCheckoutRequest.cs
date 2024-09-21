using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPatronizeBackend.API.Payment
{
    public class TierCheckoutRequest
    {
        public Guid TierId { get; set; }
    }
}
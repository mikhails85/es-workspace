using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Web.Test.Configuration;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    public class OffersController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult List(int page, int size, string search = null)
        {
            return this.Result(this.Service<IOfferManager>().GetList(page, size, search));
        }

        [HttpGet("{id}/[action]")]
        public IActionResult GetOffer(long id)
        {
            return this.Result(this.Service<IOfferManager>().GetOffer(id));
        }

        [HttpPost("[action]")]
        public IActionResult AddOffer([FromBody] Offer offer)
        {
            return this.Result(this.Service<IOfferManager>().AddOffer(offer));
        }

        [HttpPut("{id}/[action]")]
        public IActionResult UpdateOffer([FromQuery]long id, [FromBody] Offer offer)
        {
            return this.Result(this.Service<IOfferManager>().UpdateOffer(offer));
        }

        [HttpDelete("{id}/[action]")]
        public IActionResult DeleteOffer(long id)
        {
            
            return this.Result(this.Service<IOfferManager>().DeleteOffer(id));
        }
    }
}
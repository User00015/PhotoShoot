using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Services.Interfaces;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController : Controller
    {
        private readonly IEventService _eventService;

        public SquareController(IEventService  eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("Confirm")]
        public async Task<IActionResult> Checkout(string transactionId)
        {
            return Ok(await _eventService.ConfirmCheckout(transactionId));
        }

    }
}
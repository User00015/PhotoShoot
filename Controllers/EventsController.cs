using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;
using PhotoGallery.Services;
using PhotoGallery.Services.Interfaces;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {
            return Ok(await _eventService.Create(newEvent));
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetEvents());
        }

        [HttpGet("Appointment/{id}")]
        public async Task<IActionResult> GetAppointments(int id)
        {
            return Ok(await _eventService.GetAppointments(id));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEvent(id);
            return Ok(await _eventService.GetEvents());
        }
    }
}
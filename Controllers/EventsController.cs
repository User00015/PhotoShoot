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
            return Ok(await _eventService.Create(newEvent).ConfigureAwait(false));
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetEvents().ConfigureAwait(false));
        }

        [HttpGet("Appointment/{eventId}")]
        public async Task<IActionResult> GetAppointments(int eventId)
        {
            return Ok(await _eventService.GetAppointments(eventId).ConfigureAwait(false));
        }

        [HttpGet("Appointment/{eventId}/{appointmentId}")]
        public async Task<IActionResult> GetAppointment(int eventId, int appointmentId)
        {
            return Ok(await _eventService.GetAppointment(eventId, appointmentId).ConfigureAwait(false));
        }

        [HttpGet("Appointment/{eventId}/{appointmentId}/checkout")]
        public async Task<IActionResult> Checkout(int eventId, int appointmentId)
        {
            var appointment = await _eventService.GetAppointment(eventId, appointmentId).ConfigureAwait(false);
            return Ok(await _eventService.Checkout(appointment).ConfigureAwait(false));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEvent(id).ConfigureAwait(false);
            return Ok(await _eventService.GetEvents().ConfigureAwait(false));
        }
    }
}
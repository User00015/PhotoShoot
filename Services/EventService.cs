using Microsoft.EntityFrameworkCore;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Square.Connect.Model;

namespace PhotoGallery.Services
{
    public class EventService : IEventService
    {
        private readonly PhotoGalleryIdentityDbContext _context;
        private readonly ISquareService _square;

        public EventService(PhotoGalleryIdentityDbContext context, ISquareService square)
        {
            _context = context;
            _square = square;
        }

        public async Task<int> Create(Event newEvent)
        {
            int save;

            using (_context.Database.BeginTransaction())
            {
                await _context.Events.AddAsync(newEvent);
                save = _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            return save;
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events.Select(p => p).Include(p => p.Appointments).ToListAsync();
        }

        public async Task<int> DeleteEvent(int id)
        {
            int save;
            using (_context.Database.BeginTransaction())
            {
                var eventToDelete = await _context.Events.FindAsync(id);
                _context.Entry(eventToDelete).State = EntityState.Deleted;
                save = _context.SaveChanges();
                _context.Database.CommitTransaction();
            }

            return save;
        }

        public async Task<List<Appointment>> GetAppointments(int id)
        {
            return await _context.Events.Where(p => p.Id == id).SelectMany(p => p.Appointments).ToListAsync(); 
        }

        public async Task<Appointment> GetAppointment(int eventId, int appointmentId)
        {
            return await _context.Events.Where(e => e.Id == eventId).SelectMany(p => p.Appointments)
                .Where(pp => pp.Id == appointmentId).SingleOrDefaultAsync();
        }

        public async Task<string> Checkout(Appointment appointment)
        {
            return await _square.Checkout(appointment);
        }

        public async Task<RetrieveTransactionResponse> ConfirmTransaction(string transactionId)
        {
            return await _square.ConfirmCheckout(transactionId);
        }
    }
}

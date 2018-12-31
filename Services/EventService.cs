using Microsoft.EntityFrameworkCore;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using Square.Connect.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                await _context.Events.AddAsync(newEvent).ConfigureAwait(false);
                save = _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            return save;
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events.Select(p => p).Include(p => p.Appointments).ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> DeleteEvent(int id)
        {
            int save;
            using (_context.Database.BeginTransaction())
            {
                var eventToDelete = await _context.Events.FindAsync(id).ConfigureAwait(false);
                _context.Entry(eventToDelete).State = EntityState.Deleted;
                save = _context.SaveChanges();
                _context.Database.CommitTransaction();
            }

            return save;
        }

        public async Task<List<Appointment>> GetAppointments(int id)
        {
            return await _context.Events.Where(p => p.Id == id).SelectMany(p => p.Appointments).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Appointment> GetAppointment(int eventId, int appointmentId)
        {
            return await _context.Events.Where(e => e.Id == eventId).SelectMany(p => p.Appointments).Where(pp => pp.Id == appointmentId).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<string> Checkout(Appointment appointment)
        {
            return await _square.Checkout(appointment).ConfigureAwait(false);
        }

        public async Task<bool> ConfirmCheckout(string transactionId, string referenceId)
        {
            var result = await Task.WhenAll(ConfirmTransaction(transactionId), CloseAppointment(referenceId)).ConfigureAwait(false);
            return result.All(p => p);
        }

        public async Task<bool> ConfirmTransaction(string transactionId)
        {
            var confirmation = await _square.RetrieveTransaction(transactionId).ConfigureAwait(false);
            return confirmation.Transaction.Tenders.All(t => t.CardDetails.Status == TenderCardDetails.StatusEnum.CAPTURED);
        }

        private async Task<bool> CloseAppointment(string referenceId)
        {
            using (_context.Database.BeginTransaction())
            {
                var appointment = _context.Events.SelectMany(p => p.Appointments).FirstOrDefault(a => a.Id.ToString() == referenceId);
                if (appointment == null) return false;

                appointment.IsClosed = true;
                _context.Entry(appointment).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync().ConfigureAwait(false);
                _context.Database.CommitTransaction();
                return result == 1;

            }
        }
    }
}

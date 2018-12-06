using Microsoft.EntityFrameworkCore;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Services
{
    public class EventService : IEventService
    {
        private readonly PhotoGalleryIdentityDbContext _context;

        public EventService(PhotoGalleryIdentityDbContext context)
        {
            _context = context;
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

        public async Task<int> DeleteEvent(Guid id)
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
    }
}

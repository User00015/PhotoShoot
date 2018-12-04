using System;
using System.Collections.Generic;
using System.Linq;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                return await _context.Events.Select(p => p).ToListAsync();
        }

    }
}

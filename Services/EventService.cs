using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
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

            using (_context.Database.BeginTransaction())
            {
                await _context.Events.AddAsync(newEvent);
                
                var i = _context.SaveChanges();
                _context.Database.CommitTransaction();

                return i;
            }
        }
    }
}

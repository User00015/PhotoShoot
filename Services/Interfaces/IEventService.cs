using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.Entities;

namespace PhotoGallery.Services.Interfaces
{
    public interface IEventService
    {
        Task<int> Create(Event newEvent);
        Task<List<Event>> GetEvents();
        Task<int> DeleteEvent(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.Entities;
using Square.Connect.Model;

namespace PhotoGallery.Services.Interfaces
{
    public interface IEventService
    {
        Task<int> Create(Event newEvent);
        Task<List<Event>> GetEvents();
        Task<int> DeleteEvent(int id);
        Task<List<Appointment>> GetAppointments(int id);
        Task<Appointment> GetAppointment(int id, int eventId);
        Task<string> Checkout(Appointment appointment);
    }
}

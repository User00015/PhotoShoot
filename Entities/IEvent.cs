using System;
using System.Collections.Generic;

namespace PhotoGallery.Entities
{
    public interface IEvent
    {
        string Address { get; set; }
        List<Appointment> Appointments { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        string Image { get; set; }
        string Name { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
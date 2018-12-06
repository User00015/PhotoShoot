using System;
using System.Collections.Generic;

namespace PhotoGallery.Entities
{
    public interface IEvent
    {
        string Address { get; set; }
        List<Appointment> Appointments { get; set; }
        string Description { get; set; }
        Date EndDate { get; set; }
        Time EndTime { get; set; }
        int Id { get; set; }
        string Image { get; set; }
        string Name { get; set; }
        Date StartDate { get; set; }
        Time StartTime { get; set; }
    }
}
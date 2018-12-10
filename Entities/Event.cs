using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace PhotoGallery.Entities
{
    public class Event :  IEvent
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public string Display { get; set; }
        public bool IsOpen { get; set; }
        public int EventId { get; set; }
    }
}

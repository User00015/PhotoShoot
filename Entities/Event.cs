using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace PhotoGallery.Entities
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Date StartDate { get; set; }
        public Date EndDate { get; set; }
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }
        public string Image { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string Display { get; set; }
    }

    [ComplexType]
    public class Date
    {
        [Column("DateId")]
        public int DateId { get; set; }
        [Column("Year")]
        public int Year { get; set; }
        [Column("Month")]
        public int Month { get; set; }
        [Column("Day")]
        public int Day { get; set; }
    }

    [ComplexType]
    public class Time
    {
        [Column("TimeId")]
        public int TimeId { get; set; }
        [Column("Hour")]
        public int Hour { get; set; }
        [Column("Minute")]
        public int Minute { get; set; }
    }
}

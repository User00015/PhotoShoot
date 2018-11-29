﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string Display { get; set; }
    }

    public class Date
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

    public class Time
    {
        [Key]
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}

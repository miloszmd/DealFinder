﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Deals.Repository
{
    public class DealRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public UserRecord User { get; set; }
        [NotMapped]
        public double DistanceInMeters { get; set; }
    }
}
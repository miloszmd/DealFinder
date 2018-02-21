﻿using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals.Service
{
    public class DealModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public double DistanceInMeters { get; set; }
        public string UserIdentifier { get; set; }
    }
}
﻿namespace NZWalksAPI.Models.Domain
{
    public class Region
    {
        public Guid id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

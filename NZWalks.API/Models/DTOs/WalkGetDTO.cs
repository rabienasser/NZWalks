﻿namespace NZWalks.API.Models.DTOs
{
    public class WalkGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public RegionGetDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }  
        
    }
}
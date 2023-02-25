﻿using System.ComponentModel.DataAnnotations;

namespace Domain;

public class AdSpaceType
{
    public Guid AdSpaceTypeId { get; set; }
    
    [Required]
    public required string Type { get; set; }
    
    public float? Height { get; set; }
    public float? Width { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    
    public ICollection<AdSpace>? AdSpaces { get; set; }
}
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class UsersPreset
{
    public Guid UsersPresetId { get; set; }
    
    [Required]
    [ForeignKey("PresetId")]
    public Guid PresetId { get; set; }
    public required Preset Preset { get; set; }
    
    // [Required]
    // [ForeignKey("UserId")]
    // public Guid UserId { get; set; }
    public virtual required IdentityUser User { get; set; }
}

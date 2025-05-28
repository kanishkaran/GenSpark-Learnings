using System;

namespace HealthAPI.Models.DTOs;

public class DoctorAddRequestDto
{
    public string Name { get; set; } = string.Empty;
    public ICollection<SpecializationAddRequestDto>? Specialities { get; set; }
}

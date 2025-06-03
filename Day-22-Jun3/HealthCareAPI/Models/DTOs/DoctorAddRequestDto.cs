using System;

namespace HealthCareAPI.Models.DTOs;

public class DoctorAddRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public float YearOfExp { get; set; }
    public string Password { get; set; } = string.Empty;
    public ICollection<SpecializationAddRequestDto>? Specialities { get; set; }
}

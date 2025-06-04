using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Models.DTOs
{
public class DoctorResponseDto
{
    public int Id { get; set; }
    public string DoctorName { get; set; } = string.Empty;

    public float YearOfExp { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Specializations { get; set; } = new();
}
}
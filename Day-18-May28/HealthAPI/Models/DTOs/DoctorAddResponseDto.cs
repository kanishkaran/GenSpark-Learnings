using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models.DTOs
{
public class DoctorResponseDto
{
    public int Id { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<string> Specializations { get; set; } = new();
}
}
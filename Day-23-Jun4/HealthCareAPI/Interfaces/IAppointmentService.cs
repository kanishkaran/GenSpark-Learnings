using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> BookAppointment(AppointmentAddRequestDto appointment);

        Task<Appointment> CancelAppointment(AppointmentCancelRequestDto cancelRequest);
    }
}
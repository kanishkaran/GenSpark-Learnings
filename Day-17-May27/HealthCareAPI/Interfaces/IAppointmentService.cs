using System;
using HealthCareAPI.Models;

namespace HealthCareAPI.Interfaces;

public interface IAppointmentService
{
    int AddAppointment(Appointment appointment);
    int CancelAppointment(int id);
    DateTime RescheduleAppointment(Appointment appointment);
}

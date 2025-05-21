using AppointmentManagement.Models;

namespace AppointmentManagement.Interfaces
{
    public interface IAppointmentService
    {
        int AddAppointments(Appointment appointment);

        List<Appointment>? SearchAppointments(AppointmentSearchModel searchModel);
    }
}
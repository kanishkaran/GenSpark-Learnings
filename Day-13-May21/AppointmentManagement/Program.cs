

using AppointmentManagement.Interfaces;
using AppointmentManagement.Models;
using AppointmentManagement.Repositories;
using AppointmentManagement.Services;

namespace AppointmentManagement
{
    class Program
    {
        static void Main()
        {
            IRepository<int, Appointment> repository = new PatientRepository();
            IAppointmentService appointmentService = new AppointmentService(repository);
            AppointmentManager manager = new AppointmentManager(appointmentService);

            manager.Start();
        }
    }
}
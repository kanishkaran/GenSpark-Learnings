using AppointmentManagement.Interfaces;
using AppointmentManagement.Models;

namespace AppointmentManagement.Services
{
    public class AppointmentService : IAppointmentService
    {

        readonly IRepository<int, Appointment> _appointmentrepository;

        public AppointmentService(IRepository<int, Appointment> repository)
        {
            _appointmentrepository = repository;
        }
        public int AddAppointments(Appointment appointment)
        {
            try
            {
                var result = _appointmentrepository.Add(appointment);

                if (result != null)
                    return result.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public List<Appointment>? SearchAppointments(AppointmentSearchModel searchModel)
        {
            try
            {
                var appointments = _appointmentrepository.GetAll();

                appointments = GetByName(appointments, searchModel.PatientName);

                appointments = GetByDate(appointments, searchModel.AppointmentDate);

                appointments = GetByAge(appointments, searchModel.AgeRange);

                if (appointments != null && appointments.Count > 0)
                    return appointments.ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        private ICollection<Appointment> GetByAge(ICollection<Appointment> appointments, Range<int>? ageRange)
        {
            if (ageRange == null || appointments == null || appointments.Count == 0)
                return appointments;

            return appointments.Where(a => a.PatientAge >= ageRange.MinVal && a.PatientAge <= ageRange.MaxVal).ToList();
        }

        private ICollection<Appointment> GetByDate(ICollection<Appointment> appointments, DateTime? appointmentDate)
        {
            if (appointmentDate == null || appointments == null || appointments.Count == 0)
                return appointments;

            return appointments.Where(a => a.AppointmentDate == appointmentDate).ToList();
        }

        private ICollection<Appointment> GetByName(ICollection<Appointment> appointments, string? patientName)
        {
            if (patientName == null || appointments == null || appointments.Count == 0)
                return appointments;

            return appointments.Where(a => a.PatientName.ToLower().Contains(patientName.ToLower())).ToList();
        }
    }
}
using AppointmentManagement.Exceptions;
using AppointmentManagement.Models;

namespace AppointmentManagement.Repositories
{
    public class PatientRepository : Repository<int, Appointment>
    {
        public PatientRepository() : base()
        {

        }
        public override ICollection<Appointment> GetAll()
        {
            if (_items.Count == 0)
                throw new CollectionEmptyException("No Appointments Found");

            return _items;
        }

        public override Appointment GetById(int id)
        {
            var appointment = _items.FirstOrDefault(e => e.Id == id);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment Id not found");
            return appointment;

        }

        protected override int GenerateID()
        {
            if (_items.Count == 0)
                return 101;
            return _items.Max(e => e.Id) + 1;
        }


    }
}
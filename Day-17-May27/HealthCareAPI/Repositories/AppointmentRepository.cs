using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Exceptions;
using HealthCareAPI.Models;

namespace HealthCareAPI.Repositories
{
    public class AppointmentRepository : Repository<int, Appointment>
    {
        public override ICollection<Appointment> GetAll()
        {
            if (_items == null)
            {
                throw new CollectionEmptyException("The collection is empty");
            }
            return _items;
        }

        public override Appointment GetById(int id)
        {
            var appointment = _items.FirstOrDefault(a => a.Id == id) ?? throw new KeyNotFoundException("Appointment Not Found");
            return appointment;
        }

        protected override int GenerateId()
        {
            if (_items == null)
            {
                return 1001;
            }

            return _items.Max(it => it.Id) + 1;
        }
    }
}
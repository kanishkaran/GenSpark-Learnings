using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Exceptions;

namespace HealthCareAPI.Repositories
{
    public class PatientRepository : Repository<int, Patient>
    {
        public PatientRepository() : base() {}
        public override ICollection<Patient> GetAll()
        {
            if (_items == null)
            {
                throw new CollectionEmptyException("The collection is empty");
            }
            return _items;
        }

        public override Patient GetById(int id)
        {
            var patient = _items.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException("Patient Not Found");
            return patient;
        }

        protected override int GenerateId()
        {
            if (_items == null)
            {
                return 101;
            }

            return _items.Max(it => it.Id) + 1;
        }
    }
}
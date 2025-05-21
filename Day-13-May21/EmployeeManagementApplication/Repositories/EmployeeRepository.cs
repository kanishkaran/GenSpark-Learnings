using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApplication.Exceptions;
using EmployeeManagementApplication.Models;

namespace EmployeeManagementApplication.Repositories
{
    public class EmployeeRepository : Repository<int, Employee>
    {
        public EmployeeRepository() : base() { }
        public override ICollection<Employee> GetAll()
        {
            if (_items.Count == 0)
                throw new CollectionEmptyException("No Employees Found");

            return _items;
        }

        public override Employee GetById(int id)
        {
            var employee = _items.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new KeyNotFoundException("Employee Id not found");
            return employee;

        }

        protected override int GenerateID()
        {
            if (_items.Count == 0)
                return 101;
            return _items.Max(e => e.Id) + 1;
        }
    }
}
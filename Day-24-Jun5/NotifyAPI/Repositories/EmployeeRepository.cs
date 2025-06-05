using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Contexts;
using NotifyAPI.Models;

namespace NotifyAPI.Repositories
{
    public class EmployeeRepository : Repository<int, Employee>
    {
        public EmployeeRepository(NotifyDBContext context) : base(context)
        {

        }
        public async override Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Count == 0 ? throw new Exception("No employees found") : employees;
        }

        public async override Task<Employee> GetById(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);

            return employee ?? throw new Exception("employee not found");
        }
    }
}
using EmployeeManagementApplication.Interfaces;
using EmployeeManagementApplication.Models;
using EmployeeManagementApplication.Repositories;
using EmployeeManagementApplication.Services;

namespace EmployeeManagementApplication
{
    class Program
    {
        static void Main()
        {
            IRepository<int, Employee> employeeRepository = new EmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);
            ManageEmployee manageEmployee = new ManageEmployee(employeeService);
            manageEmployee.Start();
        }
    }
}
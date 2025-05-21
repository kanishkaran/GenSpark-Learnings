using EmployeeManagementApplication.Models;

namespace EmployeeManagementApplication.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(Employee employee);

        List<Employee>? SearchEmployee(SearchModel searchModel);
    }
}
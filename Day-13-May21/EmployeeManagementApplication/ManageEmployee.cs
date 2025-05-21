

using EmployeeManagementApplication.Interfaces;
using EmployeeManagementApplication.Models;

namespace EmployeeManagementApplication
{
    public class ManageEmployee
    {
        readonly IEmployeeService _employeeService;

        public ManageEmployee(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        public void Start()
        {
            while (true)
            {
                PrintMenu();
                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 && choice > 3))
                    Console.WriteLine("Enter Valid Number");

                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        SearchEmployee();
                        break;
                    case 3:
                        return;
                }
            }
        }

        private void SearchEmployee()
        {
            var searchModel = BuildeSearchModel();

            var results = _employeeService.SearchEmployee(searchModel);

            DisplaySearchResults(results);
        }

        private SearchModel BuildeSearchModel()
        {
            SearchModel searchModel = new SearchModel();


            PrintSearchMenu();
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 && choice > 4))
                Console.WriteLine("Enter Valid Number");

            switch (choice)
            {
                case 1:
                    searchModel.Id = GetIdFromUser();
                    break;
                case 2:
                    searchModel.Name = GetNameFromUser();
                    break;
                case 3:
                    searchModel.Age = GetAgeRangeFromUser();
                    break;
                case 4:
                    searchModel.Salary = GetSalaryRangeFromUser();
                    break;
            }

            return searchModel;
        }

        public static void PrintSearchMenu()
        {
            Console.WriteLine("\nSearch Menu\n\n1.Search by Id \n2.Search by Name \n3.Search by Age \n4.Search by Salary");
        }
        private int GetIdFromUser()
        {
            Console.WriteLine("Enter Employee Id to search:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Enter Valid Id");
            return id;
        }

        private string GetNameFromUser()
        {
            Console.WriteLine("Enter Employee Name to search");
            string? nameInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("Enter Valid Name");
                nameInput = Console.ReadLine();
            }

            return nameInput;
        }

        private Range<int>? GetAgeRangeFromUser()
        {
            Console.WriteLine("Enter minimum Employee Age");
            int minInput;

            while (!int.TryParse(Console.ReadLine(), out minInput))
                Console.WriteLine("Enter Valid Input");

            Console.WriteLine("Enter maximum Employee Age:");

            int maxInput;

            while (!int.TryParse(Console.ReadLine(), out maxInput))
                Console.WriteLine("Enter Valid Input");

            return new Range<int> { MinVal = minInput, MaxVal = maxInput };

        }

        private Range<double>? GetSalaryRangeFromUser()
        {
            Console.WriteLine("Enter minimum Employee Salary");
            int minSal;

            while (!int.TryParse(Console.ReadLine(), out minSal))
                Console.WriteLine("Enter Valid Input");

            Console.WriteLine("Enter maximum Employee Salary:");

            int maxSal;

            while (!int.TryParse(Console.ReadLine(), out maxSal))
                Console.WriteLine("Enter Valid Input");

            return new Range<double> { MinVal = minSal, MaxVal = maxSal };


        }

        private void AddEmployee()
        {
            Employee employee = new Employee();
            employee.TakeEmployeeDetailsFromUser();

            int result = _employeeService.AddEmployee(employee);

            if (result != -1)
                Console.WriteLine($"Employee with Id {result} Added Successfully");

        }

        private void DisplaySearchResults(List<Employee>? results)
        {
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No employees found matching the criteria.");
                return;
            }

            Console.WriteLine("Search Results:");
            foreach (var emp in results)
            {
                Console.WriteLine(emp);
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine("Please Enter your Choice");
            Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.Search Employee");
            Console.WriteLine("3.Exit");
        }
    }
}
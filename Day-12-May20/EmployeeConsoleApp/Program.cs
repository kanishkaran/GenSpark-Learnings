

using System.Collections;

namespace employee
{
    class Program()
    {
        static Dictionary<int, Employee> EmployeeDict = new Dictionary<int, Employee>();
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Select Task \n1.Easy \n2.Medium \n3.Hard \n4.Exit");
                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("enter valid number");


                switch (choice)
                {
                    case 1:
                        Execute_easy_tasks();                // To Execute Easy Task Questions
                        break;
                    case 2:
                        Execute_medium_tasks();             // To Execute Medium Task Questions
                        break;
                    case 3:
                        Execute_hard_task();                // To Execute Hard Task Questions
                        break;
                    case 4:
                        return;

                }
            }


        }


        static void Execute_easy_tasks()
        {

            EmployeePromotion obj = new EmployeePromotion();

            while (true)
            {
                Console.WriteLine("---------------------Easy Tasks---------------------------------");
                Console.WriteLine("\n1.Get Employee Promotion Order \n2.View Promotion Order \n3.View a Employee Position \n4.Trim Excess Memory \n5.View Promoted Order \n6.Back");
                Console.WriteLine("---------------------Easy Tasks---------------------------------\nEnter your choice.");

                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("enter valid number");

                switch (choice)                                 // Kindly Navigate Line 378 for the Implementations 
                {
                    case 1:
                        obj.Get_Employee_Promotion_Order();                             //Also Improved My Naming Convention :)
                        break;
                    case 2:
                        obj.View_Promotion_Order();
                        break;
                    case 3:
                        obj.Find_Employee_Position();
                        break;
                    case 4:
                        obj.Trim_Excess_Memory();
                        break;
                    case 5:
                        obj.Get_Promotion_List();
                        break;
                    case 6:
                        return;
                }

            }
        }

        static void Execute_medium_tasks()
        {

            while (true)
            {
                Console.WriteLine("---------------------Medium Tasks---------------------------------");
                Console.WriteLine("\n1.Add Employees \n2.Display Sorted Employees by Salary \n3.View Employee by id \n4.View Employee by name \n5.View Elder Employees \n6.Back");
                Console.WriteLine("---------------------Medium Tasks---------------------------------\nEnter your choice.");

                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("enter valid number");

                switch (choice)                                 // Kindly Navigate Line 158 for the Implementations
                {
                    case 1:
                        AddEmployees();
                        break;
                    case 2:
                        Display_Sorted_Employee_Salary();
                        break;
                    case 3:
                        Find_employee_by_id();
                        break;
                    case 4:
                        Find_employee_by_name();
                        break;
                    case 5:
                        Find_older_employees();
                        break;
                    case 6:
                        return;
                }

            }
        }

        static void Execute_hard_task()
        {
            while (true)
            {
                Console.WriteLine("---------------------------------Employee  Management--------------------------------------------------");
                Console.WriteLine("\n1.Display All Employees \n2.Add Employees \n3.Delete Employee \n4.Update Details \n5.Find Employee by Id \n6.Back");
                Console.WriteLine("---------------------------------Employee  Management-------------------------------------------------- \nEnter your Choice");

                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Enter Valid number");

                switch (choice)
                {
                    case 1:
                        Display_Sorted_Employee_Salary();                 // Part of Medium Task
                        break;
                    case 2:
                        AddEmployees();                                   // Part of Medium Task
                        break;
                    case 3:
                        DeleteEmployee();                                 // Kindly Navigate Line 254 for the Implementations for Delete & Update
                        break;
                    case 4:
                        UpdateEmployee();
                        break;
                    case 5:
                        Find_employee_by_id();                            // Part of Medium Task
                        break;
                    case 6:
                        Console.WriteLine("Ending");
                        return;
                }
            }
        }


    // -----------------------------------------------------Medium Tasks Below--------------------------------------------------------------------------------------------


        // Q1. Create an application that will take employee details (Use the employee class) and store it in a collection  

        // The collection should be able to give back the employee object if the employee id is provided. 

        //  The ID of employee can never be null or have duplicate values. 

        static void AddEmployees()                          // Also Used in Hard Task
        {
            Console.WriteLine("Please Enter no of employees: ");
            int no_of_employees;

            while (!int.TryParse(Console.ReadLine(), out no_of_employees))
                Console.WriteLine("Enter valid number");

            for (int emp = 0; emp < no_of_employees; emp++)
            {
                Employee employee = new Employee();

                employee.TakeEmployeeDetailsFromUser();

                EmployeeDict.Add(employee.Id, employee);

            }
        }

        // Q2. Use the application created for question 1. Store all the elements in the collection in a list. 

        // A.Sort the employees based on their salary.  

        // B.Given an employee id find the employee and print the details. 


        // Q2.A
        static void Display_Sorted_Employee_Salary()                           // Also Used in Hard Task
        {

            // AddEmployees();

            List<Employee> EmployeeList = EmployeeDict.Values.ToList();

            EmployeeList.Sort();

            foreach (Employee employee in EmployeeList)
                Console.WriteLine(employee.ToString());
        }

        // Q2.B
        static void Find_employee_by_id()                          // Also Used in Hard Task
        {
            // AddEmployees();
            Console.WriteLine("Enter Employee Id:");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Enter valid number");

            var employee = EmployeeDict.Values.Where(e => e.Id == id);

            foreach (Employee emp in employee)
                Console.WriteLine(emp.ToString());
        }
        // Q3. Use the application created for question 2. Find all the employees with the given name (Name to be taken from user) 
        static Employee Find_employee_by_name()
        {
            // AddEmployees();
            Console.WriteLine("Please Enter Employee Name: ");
            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please Enter Valid Employee Name: ");
                name = Console.ReadLine();
            }

            var employee =
                from emp in EmployeeDict.Values
                where emp.Name == name
                select emp;

            // Console.WriteLine($"Employees with {name} as Name");
            // foreach (Employee emp in employee)
            //     Console.WriteLine(emp.ToString());     // find_employee_by_id(emp.Id);

            return employee.FirstOrDefault();
        }

        //  Q4. Use the application created for question 3. Find all the employees who are elder than a given employee (Employee given by user) 
        static void Find_older_employees()
        {
            var employee = Find_employee_by_name();
            int Age = employee.Age;

            var Older =
            from emp in EmployeeDict.Values
            where emp.Age > Age
            select emp;

            Console.WriteLine("Older Employees");
            foreach (Employee older_emp in Older)
                Console.WriteLine(older_emp.ToString());

        }

    // -----------------------------------------------------Delete & Update Function For Hard Tasks Below--------------------------------------------------------------------------------------------

        static void DeleteEmployee()                          // Hard Task
        {
            Console.WriteLine("Enter Employee Id to be Deleted");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Enter valid number");

            if (!EmployeeDict.ContainsKey(id))
                Console.WriteLine("Employee not found");
            else
            {

                EmployeeDict.Remove(id);

                Console.WriteLine($"Employee {id} Deleted");
            }
        }

        static void UpdateEmployee()                          // Hard Task
        {
            Console.WriteLine("Enter Employee Id to be Updated");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Enter valid number");

            if (!EmployeeDict.ContainsKey(id))
                Console.WriteLine("Employee not found");
            else
            {
                Console.WriteLine("Enter Name: ");
                string? new_name = Console.ReadLine();

                Console.WriteLine("Enter Age: ");
                int new_age;

                while (!int.TryParse(Console.ReadLine(), out new_age))
                    Console.WriteLine("enter valid number");

                Console.WriteLine("Enter Salary");
                double new_salary;

                while (!double.TryParse(Console.ReadLine(), out new_salary))
                    Console.WriteLine("Please enter proper salary");

                var emp = EmployeeDict[id];

                emp.Age = new_age;
                emp.Salary = new_salary;
                emp.Name = new_name ?? "Not Provided";

            }

        }
    }

    class Employee : IComparable<Employee>
    {
        int id, age;
        string? name;
        double salary;

        public Employee() { }

        public Employee(int id, int age, string name, double salary)
        {
            this.id = id;
            this.age = age;
            this.name = name;
            this.salary = salary;
        }

        public void TakeEmployeeDetailsFromUser()
        {
            Console.WriteLine("Please enter the employee ID");

            id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the employee name");

            name = Console.ReadLine() ?? " ";

            Console.WriteLine("Please enter the employee age");

            age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the employee salary");

            salary = Convert.ToDouble(Console.ReadLine());
        }

        public override string ToString()
        {
            return "\n\nEmployee ID: " + id + "\nName: " + name + "\nAge: " + age + "\nSalary: " + salary;
        }

        public int CompareTo(Employee? other)
        {
            if (other is null) return 1;

            return salary.CompareTo(other.salary);
        }

        public int Id { get => id; set => id = value; }
        public int Age { get => age; set => age = value; }
        public string Name { get => name ?? " "; set => name = value; }
        public double Salary { get => salary; set => salary = value; }

    }


    // -----------------------------------------------------Easy Tasks Below--------------------------------------------------------------------------------------------
    class EmployeePromotion
    {
        public List<string> Order = new List<string>();

        // Q1. Create a C# console application which has a class with name “EmployeePromotion” that will take employee names in the order in which they are eligible for promotion.  
        public void Get_Employee_Promotion_Order()
        {
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop)");

            string? name = Console.ReadLine();

            while (true)
            {
                if (string.IsNullOrWhiteSpace(name))
                    break;
                Order.Add(name.ToLower());
                name = Console.ReadLine() ?? "";
            }

        }

        public void View_Promotion_Order()
        {
            Console.WriteLine("The Employee Order of Eligibility ");
            foreach (var name in Order)
            {
                Console.Write($"{name} ");
            }
        }

        // Q2. Use the application created for question 1 and in the same class do the following 

        // Given an employee name find his position in the promotion list 
        public void Find_Employee_Position()
        {
            Console.WriteLine("Please enter the name of the employee to check promotion position");
            string? name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid Name :( Enter valid name");
                name = Console.ReadLine();
            }

            int position = Order.IndexOf(name.ToLower());

            Console.WriteLine($"{name} is in postion {position + 1} for promotion");

        }

        // Q3.Use the application created for question 1 and in the same class do the following 

        //  The application seems to be using some excess memory for storing the name, contain the space by using only the quantity of memory that is required. 
        public void Trim_Excess_Memory()
        {
            Get_Employee_Promotion_Order();
            Console.WriteLine($"The Current Size of the Collection is {Order.Capacity}");
            Order.TrimExcess();
            Console.WriteLine($"The size after removing the extra space is {Order.Capacity}");
        }

        // Q4.Use the application created for question 1 and in the same class do the following 

        // The need for the list is over as all the employees are promoted. Not print all the employee names in ascending order. 
        public void Get_Promotion_List()
        {
            Console.WriteLine("Promoted employee list: ");
            Order.Sort();
            foreach (var name in Order)
            {
                Console.WriteLine(name);
            }
        }
    }
}




 
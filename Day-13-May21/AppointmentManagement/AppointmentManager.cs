using AppointmentManagement.Interfaces;
using AppointmentManagement.Models;

namespace AppointmentManagement
{
    public class AppointmentManager
    {
        readonly IAppointmentService _appointmentservice;

        public AppointmentManager(IAppointmentService service)
        {
            _appointmentservice = service;
        }

        public void Start()
        {

            while (true)
            {
                PrintMenu();
                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Enter Valid Number");

                switch (choice)
                {
                    case 1:
                        AddAppointment();
                        break;
                    case 2:
                        SearchAppointment();
                        break;
                    case 3:
                        return;
                }
            }
        }

        private void SearchAppointment()
        {
            AppointmentSearchModel searchModel = BuildSearchModel();

            var result = _appointmentservice.SearchAppointments(searchModel);

            DisplayAppointments(result);
        }

        private void DisplayAppointments(List<Appointment>? results)
        {
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No Patient found matching the criteria.");
                return;
            }

            Console.WriteLine("Search Results:");
            foreach (var emp in results)
            {
                Console.WriteLine(emp);
            }
        }

        private AppointmentSearchModel BuildSearchModel()
        {
            AppointmentSearchModel searchModel = new AppointmentSearchModel();


            searchModel.PatientName = GetNameFromUser();

            searchModel.AgeRange = GetAgeRangeFromUser();

            searchModel.AppointmentDate = GetDateFromUser();

            return searchModel;
        }

        private DateTime? GetDateFromUser()
        {
            Console.Write("Please Enter the Date to search (or press Enter to skip): ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return null;

            DateTime date;
            while (!DateTime.TryParse(input, out date))
            {
                Console.Write("Enter Valid Date or press Enter to skip: ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    return null;
            }
            return date;
        }

        private string? GetNameFromUser()
        {
            Console.Write("Enter Patient Name to search (or press Enter to skip): ");
            string? nameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
                return null;
            return nameInput;
        }

        private Range<int>? GetAgeRangeFromUser()
        {
            int? minAge = null, maxAge = null;

            Console.Write("Enter Min Age to search (or leave blank): ");
            var minAgeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(minAgeInput))
            {
                while (!int.TryParse(minAgeInput, out int minVal) || minVal < 0)
                {
                    Console.Write("Invalid entry. Please enter a valid Min Age (>= 0) or leave blank: ");
                    minAgeInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(minAgeInput))
                        break;
                }
                if (!string.IsNullOrWhiteSpace(minAgeInput))
                    minAge = int.Parse(minAgeInput);
            }

            Console.Write("Enter Max Age to search (or leave blank): ");
            var maxAgeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(maxAgeInput))
            {
                while (!int.TryParse(maxAgeInput, out int maxVal) || (minAge.HasValue && maxVal < minAge))
                {
                    Console.Write($"Invalid entry. Please enter a valid Max Age (>= {minAge ?? 0}) or leave blank: ");
                    maxAgeInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(maxAgeInput))
                        break;
                }
                if (!string.IsNullOrWhiteSpace(maxAgeInput))
                    maxAge = int.Parse(maxAgeInput);
            }

            if (minAge.HasValue || maxAge.HasValue)
                return new Range<int> { MinVal = minAge.Value, MaxVal = maxAge.Value };

            return null;
        }

        private void AddAppointment()
        {
            Appointment appointment = new Appointment();
            appointment.GetDetailsFromUser();

            int result = _appointmentservice.AddAppointments(appointment);

            if (result != -1)
                Console.WriteLine("Appointment Added Successfully");
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nAppointment Management");
            Console.WriteLine("1.Add Appointment");
            Console.WriteLine("2.Search Appointment");
            Console.WriteLine("3.Exit");
            Console.WriteLine("Please Enter your choice");

        }
    }
}
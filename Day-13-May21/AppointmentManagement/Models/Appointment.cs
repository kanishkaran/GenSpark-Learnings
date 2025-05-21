

namespace AppointmentManagement.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"\nId: {Id} \nPatientName: {PatientName} \nPatientAge: {PatientAge} \nAppointmentDate {AppointmentDate} \nReason: {Reason}";
        }

        public void GetDetailsFromUser()
        {
            Console.WriteLine("Please Enter the Patient Name");
            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                Console.WriteLine("Enter Valid Name");
                name = Console.ReadLine();
            }
            PatientName = name;

            Console.WriteLine("Please Enter the Patient Age");
            int age;

            while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
                Console.WriteLine("Enter Valid Age");

            PatientAge = age;

            Console.WriteLine("Please Enter the Date of Appointment");
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(), out date) || date < DateTime.Now)
                Console.WriteLine("Enter Valid Date");

            AppointmentDate = date;

            Console.WriteLine("Please state the reason for the visit");
            string? reason = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(reason) )
            {
                Console.WriteLine("Enter Valid Input");
                reason = Console.ReadLine();
            }
            Reason = reason;

        }
    }
}
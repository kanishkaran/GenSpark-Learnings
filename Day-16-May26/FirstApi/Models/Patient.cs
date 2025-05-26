

namespace FirstApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
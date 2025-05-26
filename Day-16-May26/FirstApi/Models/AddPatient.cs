using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// This model is used for adding patient without giving id expilicitly
namespace FirstApi.Models
{
    public class AddPatient
    {
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
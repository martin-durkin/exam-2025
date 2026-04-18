using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_2025.Models
{
    public class Patient
    {
        //properties
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }

        public virtual List<Appointment> Appointments { get; set; }


        //constructors
        public Patient()
        {
            Appointments = new List<Appointment>();
        }

        //methods
        public override string ToString()
        {
            return $"{Surname}, {FirstName} - {ContactNumber}";
        }
    }
}

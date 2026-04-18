using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_2025.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }  
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }



        //methods
        public override string ToString()
        {
            return $"{AppointmentDate.ToShortDateString()}";
        }
    }
}

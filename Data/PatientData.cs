using System.Data.Entity;
using exam_2025.Models;

namespace exam_2025.Data
{
    public class PatientData : DbContext
    {
        public PatientData() : base("OODExam_2025_MartinDurkin") { }

        
            public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }



    }
}

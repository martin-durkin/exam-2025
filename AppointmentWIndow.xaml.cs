using exam_2025.Data;
using exam_2025.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace exam_2025
{
    /// <summary>
    /// Interaction logic for AppointmentWIndow.xaml
    /// </summary>
    public partial class AppointmentWIndow : Window
    {
        private Patient _selectedPatient;
        private PatientData _db;
        private Appointment _selectedAppointment;
        public AppointmentWIndow()
        {
            InitializeComponent();
        }

        public AppointmentWIndow(Patient selectedPatient, PatientData db) : this()
        {
            _selectedPatient = selectedPatient;
            _db = db;
        }

        public AppointmentWIndow(Appointment selectedAppointment, PatientData db) : this()
        {
            _selectedAppointment = selectedAppointment;
            _db = db;

            //update screen with appointment info
            tpAppointmentTime.SelectedTime = _selectedAppointment.AppointmentDate;
            dpAppointmentDate.SelectedDate = _selectedAppointment.AppointmentDate.Date;
            tbxAppointmentNotes.Text = _selectedAppointment.AppointmentNotes;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read info from screen
            DateTime? timepart = tpAppointmentTime.SelectedTime;
            DateTime? datepart = dpAppointmentDate.SelectedDate;
            DateTime appointmentDateAndTime = datepart.Value.Date + timepart.Value.TimeOfDay;

            string notes = tbxAppointmentNotes.Text;

            if (_selectedPatient != null)
            {
                //create appointment object
                Appointment newAppointment = new Appointment()
                {
                    AppointmentDate = appointmentDateAndTime,
                    AppointmentNotes = notes,
                    PatientId = _selectedPatient.PatientId
                };

                //add to db
                _db.Appointments.Add(newAppointment);
                _db.SaveChanges();
                //close the window
                this.Close();

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPatient != null)
            {
                //read info from screen
                DateTime? timepart = tpAppointmentTime.SelectedTime;
                DateTime? datepart = dpAppointmentDate.SelectedDate;
                DateTime appointmentDateAndTime = datepart.Value.Date + timepart.Value.TimeOfDay;

                string notes = tbxAppointmentNotes.Text;

                //update appointment object
                _selectedAppointment.AppointmentDate = appointmentDateAndTime;
                _selectedAppointment.AppointmentNotes = notes;

                //save changes to db
                _db.Appointments.AddOrUpdate(_selectedAppointment);
                _db.SaveChanges();

                //close the window
                this.Close();
            }
        }
    }
}

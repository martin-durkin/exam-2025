using exam_2025.Data;
using exam_2025.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exam_2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PatientData db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new PatientData();

            var patients = db.Patients.ToList();

            lbxPatients.ItemsSource = patients;
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            //read info from screen
            string firstName = tbxFirstName.Text;
            string surname = tbxSurname.Text;
            string contactNumber = tbxContactNumber.Text;
            DateTime? DOB = dpDateOfBirth.SelectedDate;


            //create patient object
            Patient newPatient = new Patient()
            {
                FirstName = firstName,
                Surname = surname,
                ContactNumber = contactNumber,
                DOB = DOB ?? DateTime.MinValue
            };

            //add to db
            db.Patients.Add(newPatient);
            db.SaveChanges();

            //refresh lbx
            lbxPatients.ItemsSource = null;
            lbxPatients.ItemsSource = db.Patients.ToList();
            lbxPatients.SelectedIndex = -1;

            //clear form 
            tbxFirstName.Clear();
            tbxSurname.Clear();
            tbxContactNumber.Clear();
            dpDateOfBirth.SelectedDate = null;
        }

        private void lbxPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //determine what patient is sekected
            Patient selectedPatient = lbxPatients.SelectedItem as Patient;

            //check for null
            if (selectedPatient != null)
            {
                //display details in lbx
                tbxFirstName.Text = selectedPatient.FirstName;
                tbxSurname.Text = selectedPatient.Surname;
                tbxContactNumber.Text = selectedPatient.ContactNumber;
                dpDateOfBirth.SelectedDate = selectedPatient.DOB;

                //display appointments for patient in lbx
                var appointments = db.Appointments.Where(a => a.PatientId == selectedPatient.PatientId).ToList();

                if(appointments.Count > 0)
                {
                    lbxAppointments.ItemsSource = appointments;
                    lbxPatients.SelectedIndex = -1;
                }
                else
                {
                    lbxAppointments.ItemsSource = new string[] { "No appointments found" };
                }

            }

        }

        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            //determine patient is selected
            Patient selectedPatient = lbxPatients.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                AppointmentWIndow appointmentWindow = new AppointmentWIndow(selectedPatient, db);
                appointmentWindow.ShowDialog();

                var appointments = db.Appointments
                    .Where(a => a.PatientId == selectedPatient.PatientId)
                    .OrderByDescending(a => a.AppointmentDate).ToList();

                //refresh screen
                lbxAppointments.ItemsSource = null;
                lbxAppointments.ItemsSource = appointments;
            }
        }

        private void btnEditAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = lbxAppointments.SelectedItem as Appointment;

            if (selectedAppointment != null)
            {
                AppointmentWIndow appointmentWindow = new AppointmentWIndow(selectedAppointment, db);
                appointmentWindow.ShowDialog();

                var appointments = db.Appointments
                    .Where(a => a.PatientId == selectedAppointment.PatientId)
                    .OrderByDescending(a => a.AppointmentDate).ToList();

                //refresh screen
                lbxAppointments.ItemsSource = null;
                lbxAppointments.ItemsSource = appointments;
            }
        }
    }
}

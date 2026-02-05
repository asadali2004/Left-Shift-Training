using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
    public class HospitalManager
    {
        // Store all data
        public Dictionary<int, Patient> Patients = new Dictionary<int, Patient>();
        public Dictionary<int, Doctor> Doctors = new Dictionary<int, Doctor>();
        public List<Appointment> Appointments = new List<Appointment>();
        private int nextPatientId = 1;
        private int nextDoctorId = 1;
        private int nextAppointmentId = 1;
        
        // Add patient
        public void AddPatient(string name, int age, string bloodGroup)
        {
            int patientId = nextPatientId++;
            Patients.Add(patientId, new Patient
            {
                PatientId = patientId,
                Name = name,
                Age = age,
                BloodGroup = bloodGroup,
                MedicalHistory = new List<string>()
            });
        }
        
        // Add doctor
        public void AddDoctor(string name, string specialization)
        {
            int doctorId = nextDoctorId++;
            Doctors.Add(doctorId, new Doctor
            {
                DoctorId = doctorId,
                Name = name,
                Specialization = specialization,
                AvailableSlots = new List<DateTime>()
            });
        }
        
        // Schedule appointment
        public bool ScheduleAppointment(int patientId, int doctorId, DateTime time)
        {
            if (Patients.ContainsKey(patientId) && Doctors.ContainsKey(doctorId))
            {
                var existingAppointment = Appointments.FirstOrDefault(a => 
                    a.DoctorId == doctorId && a.AppointmentTime == time && a.Status == "Scheduled");
                
                if (existingAppointment == null)
                {
                    Appointments.Add(new Appointment
                    {
                        AppointmentId = nextAppointmentId++,
                        PatientId = patientId,
                        DoctorId = doctorId,
                        AppointmentTime = time,
                        Status = "Scheduled"
                    });
                    return true;
                }
            }
            return false;
        }
        
        // Group doctors by specialization
        public Dictionary<string, List<Doctor>> GroupDoctorsBySpecialization()
        {
            return Doctors.Values.GroupBy(d => d.Specialization).ToDictionary(g => g.Key, g => g.ToList());
        }
        
        // Get today's appointments
        public List<Appointment> GetTodayAppointments()
        {
            return Appointments.Where(a => a.AppointmentTime.Date == DateTime.Today && a.Status == "Scheduled").ToList();
        }
    }
}

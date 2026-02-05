using System;
using System.Collections.Generic;

namespace HospitalManagement
{
    public class HospitalManager
    {
        public void AddPatient(string name, int age, string bloodGroup) { }
        public void AddDoctor(string name, string specialization) { }
        public bool ScheduleAppointment(int patientId, int doctorId, DateTime time) { return false; }
        public Dictionary<string, List<Doctor>> GroupDoctorsBySpecialization() { return new Dictionary<string, List<Doctor>>(); }
        public List<Appointment> GetTodayAppointments() { return new List<Appointment>(); }
    }
}

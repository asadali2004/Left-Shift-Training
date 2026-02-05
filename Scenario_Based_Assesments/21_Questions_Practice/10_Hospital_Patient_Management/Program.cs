using System;
using System.Linq;

namespace HospitalManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalManager manager = new HospitalManager();
            
            // Add some hardcoded test data
            manager.AddPatient("John Smith", 45, "O+");
            manager.AddPatient("Sarah Johnson", 32, "A+");
            manager.AddPatient("Mike Brown", 55, "B+");
            manager.AddPatient("Emily Davis", 28, "AB+");
            manager.AddPatient("David Wilson", 38, "O-");
            
            manager.AddDoctor("Dr. James Anderson", "Cardiology");
            manager.AddDoctor("Dr. Lisa Martinez", "Neurology");
            manager.AddDoctor("Dr. Robert Taylor", "Cardiology");
            manager.AddDoctor("Dr. Maria Garcia", "Pediatrics");
            manager.AddDoctor("Dr. William Lee", "Orthopedics");
            
            // Schedule some appointments
            manager.ScheduleAppointment(1, 1, DateTime.Today.AddHours(10));
            manager.ScheduleAppointment(2, 2, DateTime.Today.AddHours(14));
            manager.ScheduleAppointment(3, 1, DateTime.Today.AddHours(15));
            manager.ScheduleAppointment(4, 4, DateTime.Today.AddHours(11));
            manager.ScheduleAppointment(5, 5, DateTime.Today.AddDays(1).AddHours(9));
            
            // Add medical history
            manager.Patients[1].MedicalHistory.Add("Hypertension diagnosed 2020");
            manager.Patients[1].MedicalHistory.Add("Regular checkup 2024");
            manager.Patients[2].MedicalHistory.Add("Migraine treatment 2023");
            
            while (true)
            {
                Console.WriteLine("\n=== Hospital Patient Management ===");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Add Doctor");
                Console.WriteLine("3. View All Patients");
                Console.WriteLine("4. View All Doctors");
                Console.WriteLine("5. View Doctors by Specialization");
                Console.WriteLine("6. Schedule Appointment");
                Console.WriteLine("7. View Today's Appointments");
                Console.WriteLine("8. View All Appointments");
                Console.WriteLine("9. Add Medical History");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice (1-10): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Patient
                    Console.WriteLine("\n--- Add Patient ---");
                    Console.Write("Enter Patient Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Enter Blood Group: ");
                    string bloodGroup = Console.ReadLine();
                    
                    manager.AddPatient(name, age, bloodGroup);
                    Console.WriteLine("Patient added successfully!");
                }
                else if (choice == "2")
                {
                    // Add Doctor
                    Console.WriteLine("\n--- Add Doctor ---");
                    Console.Write("Enter Doctor Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Specialization: ");
                    string specialization = Console.ReadLine();
                    
                    manager.AddDoctor(name, specialization);
                    Console.WriteLine("Doctor added successfully!");
                }
                else if (choice == "3")
                {
                    // View All Patients
                    Console.WriteLine("\n--- All Patients ---");
                    if (manager.Patients.Count == 0)
                    {
                        Console.WriteLine("No patients found!");
                    }
                    else
                    {
                        foreach (var patient in manager.Patients.Values)
                        {
                            Console.WriteLine($"\nID: {patient.PatientId} - {patient.Name}");
                            Console.WriteLine($"  Age: {patient.Age}, Blood Group: {patient.BloodGroup}");
                            if (patient.MedicalHistory.Count > 0)
                            {
                                Console.WriteLine("  Medical History:");
                                foreach (var history in patient.MedicalHistory)
                                {
                                    Console.WriteLine($"    - {history}");
                                }
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // View All Doctors
                    Console.WriteLine("\n--- All Doctors ---");
                    if (manager.Doctors.Count == 0)
                    {
                        Console.WriteLine("No doctors found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-5} {1,-25} {2,-20}", "ID", "Name", "Specialization");
                        Console.WriteLine(new string('-', 55));
                        foreach (var doctor in manager.Doctors.Values)
                        {
                            Console.WriteLine("{0,-5} {1,-25} {2,-20}", doctor.DoctorId, doctor.Name, doctor.Specialization);
                        }
                    }
                }
                else if (choice == "5")
                {
                    // View Doctors by Specialization
                    Console.WriteLine("\n--- Doctors by Specialization ---");
                    if (manager.Doctors.Count == 0)
                    {
                        Console.WriteLine("No doctors found!");
                    }
                    else
                    {
                        var grouped = manager.GroupDoctorsBySpecialization();
                        foreach (var specialization in grouped)
                        {
                            Console.WriteLine($"\n{specialization.Key} ({specialization.Value.Count} doctors):");
                            foreach (var doctor in specialization.Value)
                            {
                                Console.WriteLine($"  {doctor.DoctorId}. {doctor.Name}");
                            }
                        }
                    }
                }
                else if (choice == "6")
                {
                    // Schedule Appointment
                    Console.WriteLine("\n--- Schedule Appointment ---");
                    if (manager.Patients.Count == 0 || manager.Doctors.Count == 0)
                    {
                        Console.WriteLine("Need both patients and doctors to schedule!");
                    }
                    else
                    {
                        Console.Write("Enter Patient ID: ");
                        int patientId = int.Parse(Console.ReadLine());
                        
                        if (manager.Patients.ContainsKey(patientId))
                        {
                            Console.WriteLine("\nAvailable Doctors:");
                            foreach (var doctor in manager.Doctors.Values)
                            {
                                Console.WriteLine($"{doctor.DoctorId}. {doctor.Name} - {doctor.Specialization}");
                            }
                            
                            Console.Write("\nEnter Doctor ID: ");
                            int doctorId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Date (yyyy-mm-dd) or press Enter for today: ");
                            string dateInput = Console.ReadLine();
                            DateTime date = string.IsNullOrEmpty(dateInput) ? DateTime.Today : DateTime.Parse(dateInput);
                            Console.Write("Enter Time (hour 0-23): ");
                            int hour = int.Parse(Console.ReadLine());
                            DateTime appointmentTime = date.AddHours(hour);
                            
                            if (manager.ScheduleAppointment(patientId, doctorId, appointmentTime))
                            {
                                Console.WriteLine("Appointment scheduled successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed! Time slot may be taken.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Patient not found!");
                        }
                    }
                }
                else if (choice == "7")
                {
                    // View Today's Appointments
                    Console.WriteLine("\n--- Today's Appointments ---");
                    var todayAppointments = manager.GetTodayAppointments();
                    if (todayAppointments.Count == 0)
                    {
                        Console.WriteLine("No appointments today!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-20} {2,-25} {3,-15} {4,-10}", "Appt#", "Patient", "Doctor", "Time", "Status");
                        Console.WriteLine(new string('-', 85));
                        foreach (var appt in todayAppointments)
                        {
                            string patientName = manager.Patients[appt.PatientId].Name;
                            string doctorName = manager.Doctors[appt.DoctorId].Name;
                            Console.WriteLine("{0,-8} {1,-20} {2,-25} {3,-15:t} {4,-10}", 
                                appt.AppointmentId, patientName, doctorName, appt.AppointmentTime, appt.Status);
                        }
                    }
                }
                else if (choice == "8")
                {
                    // View All Appointments
                    Console.WriteLine("\n--- All Appointments ---");
                    if (manager.Appointments.Count == 0)
                    {
                        Console.WriteLine("No appointments found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-20} {2,-25} {3,-20} {4,-10}", "Appt#", "Patient", "Doctor", "Time", "Status");
                        Console.WriteLine(new string('-', 90));
                        foreach (var appt in manager.Appointments)
                        {
                            string patientName = manager.Patients[appt.PatientId].Name;
                            string doctorName = manager.Doctors[appt.DoctorId].Name;
                            Console.WriteLine("{0,-8} {1,-20} {2,-25} {3,-20:g} {4,-10}", 
                                appt.AppointmentId, patientName, doctorName, appt.AppointmentTime, appt.Status);
                        }
                    }
                }
                else if (choice == "9")
                {
                    // Add Medical History
                    Console.WriteLine("\n--- Add Medical History ---");
                    if (manager.Patients.Count == 0)
                    {
                        Console.WriteLine("No patients found!");
                    }
                    else
                    {
                        Console.Write("Enter Patient ID: ");
                        int patientId = int.Parse(Console.ReadLine());
                        
                        if (manager.Patients.ContainsKey(patientId))
                        {
                            Console.Write("Enter Medical History Entry: ");
                            string entry = Console.ReadLine();
                            manager.Patients[patientId].MedicalHistory.Add(entry);
                            Console.WriteLine("Medical history updated!");
                        }
                        else
                        {
                            Console.WriteLine("Patient not found!");
                        }
                    }
                }
                else if (choice == "10")
                {
                    Console.WriteLine("Thank you!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}

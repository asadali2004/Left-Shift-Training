# Question 10: Hospital Patient Management

## Scenario
A hospital needs to manage patient records and appointments.

## Requirements

### In class Patient:
- `int PatientId`
- `string Name`
- `int Age`
- `string BloodGroup`
- `List<string> MedicalHistory`

### In class Doctor:
- `int DoctorId`
- `string Name`
- `string Specialization`
- `List<DateTime> AvailableSlots`

### In class Appointment:
- `int AppointmentId`
- `int PatientId`
- `int DoctorId`
- `DateTime AppointmentTime`
- `string Status` (Scheduled/Completed/Cancelled)

### In class HospitalManager:

#### Method 1
```csharp
public void AddPatient(string name, int age, string bloodGroup)
```

#### Method 2
```csharp
public void AddDoctor(string name, string specialization)
```

#### Method 3
```csharp
public bool ScheduleAppointment(int patientId, int doctorId, DateTime time)
```

#### Method 4
```csharp
public Dictionary<string, List<Doctor>> GroupDoctorsBySpecialization()
```

#### Method 5
```csharp
public List<Appointment> GetTodayAppointments()
```

## Sample Use Cases:
1. Register patients and doctors
2. Schedule appointments
3. View doctors by specialization
4. Check daily appointments
5. Manage medical history
csharp
// In class Patient:
// - int PatientId
// - string Name
// - int Age
// - string BloodGroup
// - List<string> MedicalHistory

// In class Doctor:
// - int DoctorId
// - string Name
// - string Specialization
// - List<DateTime> AvailableSlots

// In class Appointment:
// - int AppointmentId
// - int PatientId
// - int DoctorId
// - DateTime AppointmentTime
// - string Status (Scheduled/Completed/Cancelled)

// In class HospitalManager:
public void AddPatient(string name, int age, string bloodGroup)
public void AddDoctor(string name, string specialization)
public bool ScheduleAppointment(int patientId, int doctorId, DateTime time)
public Dictionary<string, List<Doctor>> GroupDoctorsBySpecialization()
public List<Appointment> GetTodayAppointments()
Use Cases:
•	Register patients and doctors
•	Schedule appointments
•	View doctors by specialization
•	Check daily appointments
•	Manage medical history

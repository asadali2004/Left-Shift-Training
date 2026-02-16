public interface IPatient
{
    int PatientId { get; }
    string Name { get; }
    DateTime DateOfBirth { get; }
    BloodType BloodType { get; }
}

public enum BloodType { A, B, AB, O }
public enum Condition { Stable, Critical, Recovering }

// 1. Generic patient queue with priority
public class PriorityQueue<T> where T : IPatient
{
    private SortedDictionary<int, Queue<T>> _queues = new();
    
    // TODO: Enqueue patient with priority (1=highest, 5=lowest)
    public void Enqueue(T patient, int priority)
    {
        // Validate priority range
        if (priority < 1 || priority > 5)
            throw new ArgumentException("Priority must be between 1 and 5");
        
        // Create queue if doesn't exist for priority
        if (!_queues.ContainsKey(priority))
            _queues[priority] = new Queue<T>();
        
        _queues[priority].Enqueue(patient);
    }
    
    // TODO: Dequeue highest priority patient
    public T Dequeue()
    {
        // Return patient from highest non-empty priority
        // Throw if empty
        foreach (var kvp in _queues)
        {
            if (kvp.Value.Count > 0)
                return kvp.Value.Dequeue();
        }
        throw new InvalidOperationException("Queue is empty");
    }
    
    // TODO: Peek without removing
    public T Peek()
    {
        // Look at next patient
        foreach (var kvp in _queues)
        {
            if (kvp.Value.Count > 0)
                return kvp.Value.Peek();
        }
        throw new InvalidOperationException("Queue is empty");
    }
    
    // TODO: Get count by priority
    public int GetCountByPriority(int priority)
    {
        // Return count for specific priority
        if (_queues.ContainsKey(priority))
            return _queues[priority].Count;
        return 0;
    }
}

// 2. Generic medical record
public class MedicalRecord<T> where T : IPatient
{
    private T _patient;
    private List<string> _diagnoses = new();
    private Dictionary<DateTime, string> _treatments = new();
    
    public MedicalRecord(T patient)
    {
        _patient = patient;
    }
    
    // TODO: Add diagnosis with date
    public void AddDiagnosis(string diagnosis, DateTime date)
    {
        // Add to diagnoses list
        _diagnoses.Add($"[{date:yyyy-MM-dd}] {diagnosis}");
    }
    
    // TODO: Add treatment
    public void AddTreatment(string treatment, DateTime date)
    {
        // Add to treatments dictionary
        _treatments[date] = treatment;
    }
    
    // TODO: Get treatment history
    public IEnumerable<KeyValuePair<DateTime, string>> GetTreatmentHistory()
    {
        // Return sorted by date
        return _treatments.OrderBy(x => x.Key);
    }
    
    public IEnumerable<string> GetDiagnoses()
    {
        return _diagnoses;
    }
}

// 3. Specialized patient types
public class PediatricPatient : IPatient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public BloodType BloodType { get; set; }
    public string? GuardianName { get; set; }
    public double Weight { get; set; } // in kg
}

public class GeriatricPatient : IPatient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public BloodType BloodType { get; set; }
    public List<string> ChronicConditions { get; } = new();
    public int MobilityScore { get; set; } // 1-10
}

// 4. Generic medication system
public class MedicationSystem<T> where T : IPatient
{
    private Dictionary<T, List<(string medication, DateTime time)>> _medications = new();
    
    // TODO: Prescribe medication with dosage validation
    public void PrescribeMedication(T patient, string medication, 
        Func<T, bool> dosageValidator)
    {
        // Check if dosage is valid for patient type
        // Pediatric: weight-based validation
        // Geriatric: kidney function consideration
        if (!dosageValidator(patient))
            throw new InvalidOperationException("Dosage validation failed for patient");
        
        if (!_medications.ContainsKey(patient))
            _medications[patient] = new List<(string, DateTime)>();
        
        _medications[patient].Add((medication, DateTime.Now));
    }
    
    // TODO: Check for drug interactions
    public bool CheckInteractions(T patient, string newMedication)
    {
        // Return true if interaction with existing medications
        if (!_medications.ContainsKey(patient))
            return false;
        
        // Simple interaction checking logic
        var commonInteractions = new[]
        {
            ("Aspirin", "Ibuprofen"),
            ("Warfarin", "Aspirin"),
            ("Metformin", "Alcohol")
        };
        
        foreach (var (med, _) in _medications[patient])
        {
            foreach (var (drug1, drug2) in commonInteractions)
            {
                if ((med.Contains(drug1) && newMedication.Contains(drug2)) ||
                    (med.Contains(drug2) && newMedication.Contains(drug1)))
                {
                    return true; // Interaction found
                }
            }
        }
        
        return false; // No interaction
    }
    
    public List<(string medication, DateTime time)> GetMedications(T patient)
    {
        if (_medications.ContainsKey(patient))
            return _medications[patient];
        return new List<(string, DateTime)>();
    }
}

// 5. TEST SCENARIO: Simulate hospital workflow
// a) Create 2 PediatricPatient and 2 GeriatricPatient
// b) Add them to priority queue with different priorities
// c) Create medical records with diagnoses/treatments
// d) Prescribe medications with type-specific validation
// e) Demonstrate:
//    - Priority-based patient processing
//    - Age-specific medication validation
//    - Treatment history retrieval
//    - Drug interaction checking

class Program
{
    static void Main()
    {
        Console.WriteLine("========== HOSPITAL PATIENT MANAGEMENT SYSTEM ==========\n");
        
        // Create priority queue
        var patientQueue = new PriorityQueue<IPatient>();
        var medicalRecords = new Dictionary<int, MedicalRecord<IPatient>>();
        var medicationSystem = new MedicationSystem<IPatient>();
        
        // a) Create 2 PediatricPatient and 2 GeriatricPatient
        Console.WriteLine("--- Creating Patients ---");
        
        var pediatricPatient1 = new PediatricPatient
        {
            PatientId = 101,
            Name = "Emma Johnson",
            DateOfBirth = new DateTime(2020, 5, 15),
            BloodType = BloodType.A,
            GuardianName = "Sarah Johnson",
            Weight = 25.5
        };
        
        var pediatricPatient2 = new PediatricPatient
        {
            PatientId = 102,
            Name = "Liam Smith",
            DateOfBirth = new DateTime(2019, 8, 22),
            BloodType = BloodType.O,
            GuardianName = "Michael Smith",
            Weight = 30.0
        };
        
        var geriatricPatient1 = new GeriatricPatient
        {
            PatientId = 201,
            Name = "Robert Williams",
            DateOfBirth = new DateTime(1945, 3, 10),
            BloodType = BloodType.B,
            MobilityScore = 6
        };
        geriatricPatient1.ChronicConditions.Add("Diabetes");
        geriatricPatient1.ChronicConditions.Add("Hypertension");
        
        var geriatricPatient2 = new GeriatricPatient
        {
            PatientId = 202,
            Name = "Margaret Brown",
            DateOfBirth = new DateTime(1942, 11, 8),
            BloodType = BloodType.AB,
            MobilityScore = 4
        };
        geriatricPatient2.ChronicConditions.Add("Heart Disease");
        
        Console.WriteLine($"Created: {pediatricPatient1.Name} (Age: {GetAge(pediatricPatient1.DateOfBirth)}, Weight: {pediatricPatient1.Weight}kg)");
        Console.WriteLine($"Created: {pediatricPatient2.Name} (Age: {GetAge(pediatricPatient2.DateOfBirth)}, Weight: {pediatricPatient2.Weight}kg)");
        Console.WriteLine($"Created: {geriatricPatient1.Name} (Age: {GetAge(geriatricPatient1.DateOfBirth)}, Conditions: {string.Join(", ", geriatricPatient1.ChronicConditions)})");
        Console.WriteLine($"Created: {geriatricPatient2.Name} (Age: {GetAge(geriatricPatient2.DateOfBirth)}, Conditions: {string.Join(", ", geriatricPatient2.ChronicConditions)})");
        
        // b) Add them to priority queue with different priorities
        Console.WriteLine("\n--- Adding to Priority Queue ---");
        patientQueue.Enqueue(pediatricPatient1, 3);
        Console.WriteLine($"Added {pediatricPatient1.Name} - Priority: 3");
        
        patientQueue.Enqueue(geriatricPatient1, 1);
        Console.WriteLine($"Added {geriatricPatient1.Name} - Priority: 1 (Highest)");
        
        patientQueue.Enqueue(pediatricPatient2, 4);
        Console.WriteLine($"Added {pediatricPatient2.Name} - Priority: 4");
        
        patientQueue.Enqueue(geriatricPatient2, 2);
        Console.WriteLine($"Added {geriatricPatient2.Name} - Priority: 2");
        
        // c) Create medical records with diagnoses/treatments
        Console.WriteLine("\n--- Creating Medical Records ---");
        
        var record1 = new MedicalRecord<IPatient>(pediatricPatient1);
        record1.AddDiagnosis("Common Cold", DateTime.Now.AddDays(-2));
        record1.AddDiagnosis("Mild Fever", DateTime.Now.AddDays(-1));
        record1.AddTreatment("Paracetamol 250mg", DateTime.Now.AddDays(-1));
        record1.AddTreatment("Rest and fluids", DateTime.Now);
        medicalRecords[pediatricPatient1.PatientId] = record1;
        Console.WriteLine($"Created record for {pediatricPatient1.Name}");
        
        var record2 = new MedicalRecord<IPatient>(geriatricPatient1);
        record2.AddDiagnosis("High Blood Pressure", DateTime.Now.AddDays(-5));
        record2.AddDiagnosis("Chest Discomfort", DateTime.Now.AddDays(-2));
        record2.AddTreatment("Enalapril 5mg", DateTime.Now.AddDays(-5));
        record2.AddTreatment("Aspirin 75mg", DateTime.Now.AddDays(-2));
        medicalRecords[geriatricPatient1.PatientId] = record2;
        Console.WriteLine($"Created record for {geriatricPatient1.Name}");
        
        // d) Prescribe medications with type-specific validation
        Console.WriteLine("\n--- Prescribing Medications ---");
        
        // Pediatric medication validation (weight-based)
        Func<IPatient, bool> pediatricValidator = (patient) =>
        {
            if (patient is PediatricPatient pp)
                return pp.Weight > 20; // Must be above 20kg
            return true;
        };
        
        try
        {
            medicationSystem.PrescribeMedication(pediatricPatient1, "Ibuprofen 100mg", pediatricValidator);
            Console.WriteLine($"✓ Prescribed Ibuprofen 100mg to {pediatricPatient1.Name} (Weight: {pediatricPatient1.Weight}kg)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
        }
        
        // Geriatric medication validation (kidney function consideration)
        Func<IPatient, bool> geriatricValidator = (patient) =>
        {
            if (patient is GeriatricPatient gp)
                return gp.MobilityScore >= 4; // Must have mobility score >= 4
            return true;
        };
        
        try
        {
            medicationSystem.PrescribeMedication(geriatricPatient1, "Metformin 500mg", geriatricValidator);
            Console.WriteLine($"✓ Prescribed Metformin 500mg to {geriatricPatient1.Name} (Mobility: {geriatricPatient1.MobilityScore}/10)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
        }
        
        // e) Demonstrate functionality
        Console.WriteLine("\n--- Demonstration: Priority-based Patient Processing ---");
        Console.WriteLine($"Queue status - Priority 1: {patientQueue.GetCountByPriority(1)} patient(s)");
        Console.WriteLine($"Queue status - Priority 2: {patientQueue.GetCountByPriority(2)} patient(s)");
        Console.WriteLine($"Queue status - Priority 3: {patientQueue.GetCountByPriority(3)} patient(s)");
        Console.WriteLine($"Queue status - Priority 4: {patientQueue.GetCountByPriority(4)} patient(s)");
        
        Console.WriteLine("\n--- Processing Patients by Priority ---");
        for (int i = 0; i < 4; i++)
        {
            try
            {
                var patient = patientQueue.Dequeue();
                Console.WriteLine($"Processing: {patient.Name} (ID: {patient.PatientId}, Blood Type: {patient.BloodType})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        Console.WriteLine("\n--- Treatment History Retrieval ---");
        if (medicalRecords.ContainsKey(pediatricPatient1.PatientId))
        {
            var treatments = medicalRecords[pediatricPatient1.PatientId].GetTreatmentHistory();
            Console.WriteLine($"\nTreatment History for {pediatricPatient1.Name}:");
            foreach (var (date, treatment) in treatments)
            {
                Console.WriteLine($"  {date:yyyy-MM-dd HH:mm}: {treatment}");
            }
        }
        
        Console.WriteLine("\n--- Drug Interaction Checking ---");
        try
        {
            bool hasInteraction = medicationSystem.CheckInteractions(geriatricPatient1, "Warfarin");
            if (hasInteraction)
            {
                Console.WriteLine($"⚠ Warning: {geriatricPatient1.Name} has medication interaction with Warfarin!");
            }
            else
            {
                Console.WriteLine($"✓ No interaction detected for {geriatricPatient1.Name} with Warfarin");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking interactions: {ex.Message}");
        }
        
        Console.WriteLine("\n========== END OF SIMULATION ==========");
    }
    
    static int GetAge(DateTime dateOfBirth)
    {
        int age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age--;
        return age;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class ApplicantService
{
    private const string FilePath = "applicants.json";

    private List<Applicant> applicants = new List<Applicant>();

    public ApplicantService()
    {
        LoadFromFile();
    }

    // ---------------- VALIDATION ----------------

    private void Validate(Applicant a)
    {
        if (string.IsNullOrWhiteSpace(a.ApplicantId) ||
            string.IsNullOrWhiteSpace(a.Name) ||
            string.IsNullOrWhiteSpace(a.CurrentLocation) ||
            string.IsNullOrWhiteSpace(a.PreferredLocation) ||
            string.IsNullOrWhiteSpace(a.CoreCompetency))
        {
            throw new Exception("All fields are mandatory.");
        }

        if (a.ApplicantId.Length != 8 || !a.ApplicantId.StartsWith("CH"))
            throw new Exception("Applicant ID must start with CH and be exactly 8 characters.");

        if (a.Name.Length < 4 || a.Name.Length > 15)
            throw new Exception("Name must be between 4 and 15 characters.");

        if (a.PassingYear > DateTime.Now.Year)
            throw new Exception("Passing year cannot be in the future.");
    }

    // ---------------- CRUD ----------------

    public void Add(Applicant a)
    {
        Validate(a);

        if (applicants.Any(x => x.ApplicantId == a.ApplicantId))
            throw new Exception("Applicant ID already exists.");

        applicants.Add(a);
        SaveToFile();
    }

    public List<Applicant> GetAll() => applicants;

    public Applicant? GetById(string id)
    {
        return applicants.FirstOrDefault(x => x.ApplicantId == id);
    }

    public void Update(string id, Applicant updated)
    {
        var existing = GetById(id);
        if (existing == null)
            throw new Exception("Applicant not found.");

        Validate(updated);

        existing.Name = updated.Name;
        existing.CurrentLocation = updated.CurrentLocation;
        existing.PreferredLocation = updated.PreferredLocation;
        existing.CoreCompetency = updated.CoreCompetency;
        existing.PassingYear = updated.PassingYear;

        SaveToFile();
    }

    public void Delete(string id)
    {
        var applicant = GetById(id);
        if (applicant == null)
            throw new Exception("Applicant not found.");

        applicants.Remove(applicant);
        SaveToFile();
    }

    // ---------------- FILE HANDLING ----------------

    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(applicants, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(FilePath, json);
    }

    private void LoadFromFile()
    {
        if (!File.Exists(FilePath)) return;

        var json = File.ReadAllText(FilePath);

        var data = JsonSerializer.Deserialize<List<Applicant>>(json);

        if (data != null)
            applicants = data;
    }
}

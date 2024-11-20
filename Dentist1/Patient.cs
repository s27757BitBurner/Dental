using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Patient : Person
    {
        private static List<Patient> extent = new List<Patient>();

        public string PatientID { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsuranceNumber { get; set; }

        public Patient(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                       string patientID, string insuranceProvider, string insuranceNumber, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, middleName)
        {
            PatientID = patientID;
            InsuranceProvider = insuranceProvider;
            InsuranceNumber = insuranceNumber;
            AddToExtent(this);
        }

        private static void AddToExtent(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }
            extent.Add(patient);
        }

        public static List<Patient> GetExtent()
        {
            return new List<Patient>(extent);
        }

        public static void SaveExtent(string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(extent, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving extent: {ex.Message}");
            }
        }

        public static void LoadExtent(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    extent = JsonSerializer.Deserialize<List<Patient>>(json) ?? new List<Patient>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Patient>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Patient>();
            }
        }

        public static void DisplayAllPatients()
        {
            foreach (var patient in extent)
            {
                Console.WriteLine($"PatientID: {patient.PatientID}, Name: {patient.FirstName} {patient.LastName}, Insurance: {patient.InsuranceProvider}");
            }
        }
    }
}

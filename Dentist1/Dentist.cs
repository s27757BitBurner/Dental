using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Dentist : DentalProfessional
    {
        private static List<Dentist> extent = new List<Dentist>();

        public string ConsultationType { get; set; }

        public Dentist(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                       string staffID, DateTime hireDate, double salary, string workingHours, bool isFullTime, string status, string accessLevel,
                       string licenseNumber, string specialization, int yearsOfExperience, string consultationType, StaffMember supervisor = null, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, staffID, hireDate, salary, workingHours, isFullTime, status, accessLevel, licenseNumber, specialization, yearsOfExperience, supervisor, middleName)
        {
            ConsultationType = consultationType;

            AddToExtent(this);
        }

        private static void AddToExtent(Dentist dentist)
        {
            if (dentist == null)
            {
                throw new ArgumentException("Dentist cannot be null");
            }
            extent.Add(dentist);
        }

        public static List<Dentist> GetExtent()
        {
            return new List<Dentist>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Dentist>>(json) ?? new List<Dentist>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Dentist>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Dentist>();
            }
        }

        public static void DisplayAllDentists()
        {
            foreach (var dentist in extent)
            {
                Console.WriteLine($"Name: {dentist.FirstName} {dentist.LastName}, Consultation Type: {dentist.ConsultationType}");
            }
        }
    }
}

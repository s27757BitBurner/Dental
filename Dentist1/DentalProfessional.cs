using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class DentalProfessional : StaffMember
    {
        private static List<DentalProfessional> extent = new List<DentalProfessional>();

        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }

        public DentalProfessional(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                                  string staffID, DateTime hireDate, double salary, string workingHours, bool isFullTime, string status, string accessLevel,
                                  string licenseNumber, string specialization, int yearsOfExperience, StaffMember supervisor = null, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, staffID, hireDate, salary, workingHours, isFullTime, status, accessLevel, supervisor, middleName)
        {
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            YearsOfExperience = yearsOfExperience;

            AddToExtent(this);
        }

        private static void AddToExtent(DentalProfessional dentalProfessional)
        {
            if (dentalProfessional == null)
            {
                throw new ArgumentException("DentalProfessional cannot be null");
            }
            extent.Add(dentalProfessional);
        }

        public static List<DentalProfessional> GetExtent()
        {
            return new List<DentalProfessional>(extent);
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
                    extent = JsonSerializer.Deserialize<List<DentalProfessional>>(json) ?? new List<DentalProfessional>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<DentalProfessional>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<DentalProfessional>();
            }
        }

        public static void DisplayAllDentalProfessionals()
        {
            foreach (var professional in extent)
            {
                Console.WriteLine($"Name: {professional.FirstName} {professional.LastName}, Specialization: {professional.Specialization}, Years of Experience: {professional.YearsOfExperience}");
            }
        }
    }
}

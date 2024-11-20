using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Hygienist : DentalProfessional
    {
        private static List<Hygienist> extent = new List<Hygienist>();

        public bool PerformCleaning { get; set; }

        public Hygienist(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                         string staffID, DateTime hireDate, double salary, string workingHours, bool isFullTime, string status, string accessLevel,
                         string licenseNumber, string specialization, int yearsOfExperience, bool performCleaning, StaffMember supervisor = null, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, staffID, hireDate, salary, workingHours, isFullTime, status, accessLevel, licenseNumber, specialization, yearsOfExperience, supervisor, middleName)
        {
            PerformCleaning = performCleaning;

            AddToExtent(this);
        }

        private static void AddToExtent(Hygienist hygienist)
        {
            if (hygienist == null)
            {
                throw new ArgumentException("Hygienist cannot be null");
            }
            extent.Add(hygienist);
        }

        public static List<Hygienist> GetExtent()
        {
            return new List<Hygienist>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Hygienist>>(json) ?? new List<Hygienist>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Hygienist>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Hygienist>();
            }
        }

        public static void DisplayAllHygienists()
        {
            foreach (var hygienist in extent)
            {
                Console.WriteLine($"Name: {hygienist.FirstName} {hygienist.LastName}, Perform Cleaning: {hygienist.PerformCleaning}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class LabTechnician : DentalProfessional
    {
        private static List<LabTechnician> extent = new List<LabTechnician>();

        public string WorkShift { get; set; }
        public string LaboratoryEquipmentSkills { get; set; }

        public LabTechnician(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                             string staffID, DateTime hireDate, double salary, string workingHours, bool isFullTime, string status, string accessLevel,
                             string licenseNumber, string specialization, int yearsOfExperience, string workShift, string laboratoryEquipmentSkills, StaffMember supervisor = null, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, staffID, hireDate, salary, workingHours, isFullTime, status, accessLevel, licenseNumber, specialization, yearsOfExperience, supervisor, middleName)
        {
            WorkShift = workShift;
            LaboratoryEquipmentSkills = laboratoryEquipmentSkills;

            AddToExtent(this);
        }

        private static void AddToExtent(LabTechnician labTechnician)
        {
            if (labTechnician == null)
            {
                throw new ArgumentException("LabTechnician cannot be null");
            }
            extent.Add(labTechnician);
        }

        public static List<LabTechnician> GetExtent()
        {
            return new List<LabTechnician>(extent);
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
                    extent = JsonSerializer.Deserialize<List<LabTechnician>>(json) ?? new List<LabTechnician>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<LabTechnician>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<LabTechnician>();
            }
        }

        public static void DisplayAllLabTechnicians()
        {
            foreach (var labTech in extent)
            {
                Console.WriteLine($"Name: {labTech.FirstName} {labTech.LastName}, Work Shift: {labTech.WorkShift}, Skills: {labTech.LaboratoryEquipmentSkills}");
            }
        }
    }
}

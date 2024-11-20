using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class StaffMember : Person
    {
        private static List<StaffMember> extent = new List<StaffMember>();

        public string StaffID { get; set; }
        public DateTime HireDate { get; set; }
        public double Salary { get; set; }
        public string WorkingHours { get; set; }
        public bool IsFullTime { get; set; }
        public string Status { get; set; }
        public string AccessLevel { get; set; }
        public StaffMember Supervisor { get; set; }

        public StaffMember(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email,
                           string staffID, DateTime hireDate, double salary, string workingHours, bool isFullTime, string status, string accessLevel,
                           StaffMember supervisor = null, string middleName = null)
            : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber, email, middleName)
        {
            StaffID = staffID;
            HireDate = hireDate;
            Salary = salary;
            WorkingHours = workingHours;
            IsFullTime = isFullTime;
            Status = status;
            AccessLevel = accessLevel;
            Supervisor = supervisor;

            AddToExtent(this);
        }

        private static void AddToExtent(StaffMember staffMember)
        {
            if (staffMember == null)
            {
                throw new ArgumentException("StaffMember cannot be null");
            }
            extent.Add(staffMember);
        }

        public static List<StaffMember> GetExtent()
        {
            return new List<StaffMember>(extent);
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
                    extent = JsonSerializer.Deserialize<List<StaffMember>>(json) ?? new List<StaffMember>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<StaffMember>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<StaffMember>();
            }
        }

        public static void DisplayAllStaffMembers()
        {
            foreach (var staff in extent)
            {
                Console.WriteLine($"StaffID: {staff.StaffID}, Name: {staff.FirstName} {staff.LastName}, Status: {staff.Status}, Access Level: {staff.AccessLevel}");
            }
        }
    }
}

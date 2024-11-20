using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class DentalTreatment : IDentalPlan, IMedicalTreatment
    {
        private static List<DentalTreatment> extent = new List<DentalTreatment>();

        public string PlanID { get; set; }
        public string PlanType { get; set; }
        public double Coverage { get; set; }

        public string TreatmentID { get; set; }
        public string TreatmentName { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string AverageDuration { get; set; }

        public double Duration { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public Bill GeneratedBill { get; set; }
        public List<Medication> RequiredMedications { get; set; }

        public DentalTreatment(
            string planID, string planType, double coverage,
            string treatmentID, string treatmentName, int cost, string description, string averageDuration,
            double duration, DateTime date, TimeSpan time)
        {
            PlanID = planID;
            PlanType = planType;
            Coverage = coverage;

            TreatmentID = treatmentID;
            TreatmentName = treatmentName;
            Cost = cost;
            Description = description;
            AverageDuration = averageDuration;

            Duration = duration;
            Date = date;
            Time = time;

            RequiredMedications = new List<Medication>();
            GeneratedBill = new Bill(GenerateUniqueBillingID(), cost, DateTime.Now, "Pending");

            AddToExtent(this);
        }

        private static void AddToExtent(DentalTreatment dentalTreatment)
        {
            if (dentalTreatment == null)
            {
                throw new ArgumentException("DentalTreatment cannot be null");
            }
            extent.Add(dentalTreatment);
        }

        public static List<DentalTreatment> GetExtent()
        {
            return new List<DentalTreatment>(extent);
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
                    extent = JsonSerializer.Deserialize<List<DentalTreatment>>(json) ?? new List<DentalTreatment>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<DentalTreatment>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<DentalTreatment>();
            }
        }

        public static void DisplayAllDentalTreatments()
        {
            foreach (var treatment in extent)
            {
                Console.WriteLine($"Treatment ID: {treatment.TreatmentID}, Name: {treatment.TreatmentName}, Cost: {treatment.Cost}, Date: {treatment.Date}");
            }
        }

        private int GenerateUniqueBillingID()
        {
            return new Random().Next(1000, 9999);
        }

        public void AddMedication(Medication medication)
        {
            RequiredMedications.Add(medication);
        }
    }
}

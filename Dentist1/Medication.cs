using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Medication
    {
        private static List<Medication> extent = new List<Medication>();

        public int IdMedication { get; set; }
        public string Name { get; set; }

        public Medication(int idMedication, string name)
        {
            IdMedication = idMedication;
            Name = name;

            AddToExtent(this);
        }

        private static void AddToExtent(Medication medication)
        {
            if (medication == null)
            {
                throw new ArgumentException("Medication cannot be null");
            }
            extent.Add(medication);
        }

        public static List<Medication> GetExtent()
        {
            return new List<Medication>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Medication>>(json) ?? new List<Medication>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Medication>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Medication>();
            }
        }

        public static void DisplayAllMedications()
        {
            foreach (var medication in extent)
            {
                Console.WriteLine($"Medication ID: {medication.IdMedication}, Name: {medication.Name}");
            }
        }
    }
}

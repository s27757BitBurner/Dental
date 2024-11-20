using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class DentalRecord
    {
        private static List<DentalRecord> extent = new List<DentalRecord>();

        public string RecordID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public Patient BelongsToPatient { get; set; }

        public DentalRecord(string recordID, DateTime dateCreated, DateTime lastUpdated, Patient patient)
        {
            RecordID = recordID;
            DateCreated = dateCreated;
            LastUpdated = lastUpdated;
            BelongsToPatient = patient;

            AddToExtent(this);
        }

        private static void AddToExtent(DentalRecord record)
        {
            if (record == null)
            {
                throw new ArgumentException("DentalRecord cannot be null");
            }
            extent.Add(record);
        }

        public static List<DentalRecord> GetExtent()
        {
            return new List<DentalRecord>(extent);
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
                    extent = JsonSerializer.Deserialize<List<DentalRecord>>(json) ?? new List<DentalRecord>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<DentalRecord>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<DentalRecord>();
            }
        }

        public static void DisplayAllDentalRecords()
        {
            foreach (var record in extent)
            {
                Console.WriteLine($"Record ID: {record.RecordID}, Created: {record.DateCreated}, Last Updated: {record.LastUpdated}");
            }
        }
    }
}

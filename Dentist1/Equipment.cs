using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Equipment
    {
        private static List<Equipment> extent = new List<Equipment>();

        public int IdEquipment { get; set; }
        public string Name { get; set; }

        public Equipment(int idEquipment, string name)
        {
            IdEquipment = idEquipment;
            Name = name;

            AddToExtent(this);
        }

        private static void AddToExtent(Equipment equipment)
        {
            if (equipment == null)
            {
                throw new ArgumentException("Equipment cannot be null");
            }
            extent.Add(equipment);
        }

        public static List<Equipment> GetExtent()
        {
            return new List<Equipment>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Equipment>>(json) ?? new List<Equipment>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Equipment>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Equipment>();
            }
        }

        public static void DisplayAllEquipment()
        {
            foreach (var equipment in extent)
            {
                Console.WriteLine($"Equipment ID: {equipment.IdEquipment}, Name: {equipment.Name}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Room
    {
        private static List<Room> extent = new List<Room>();

        public int Number { get; set; }
        public int EquipmentCapacity { get; set; } = 5;
        public int Floor { get; set; }
        public List<Equipment> EquipmentUsed { get; set; }

        public Room(int number, int floor)
        {
            Number = number;
            Floor = floor;
            EquipmentUsed = new List<Equipment>();

            AddToExtent(this);
        }

        private static void AddToExtent(Room room)
        {
            if (room == null)
            {
                throw new ArgumentException("Room cannot be null");
            }
            extent.Add(room);
        }

        public static List<Room> GetExtent()
        {
            return new List<Room>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Room>>(json) ?? new List<Room>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Room>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Room>();
            }
        }

        public static void DisplayAllRooms()
        {
            foreach (var room in extent)
            {
                Console.WriteLine($"Room Number: {room.Number}, Floor: {room.Floor}, Equipment Capacity: {room.EquipmentCapacity}");
            }
        }

        public void AddEquipment(Equipment equipment)
        {
            if (EquipmentUsed.Count < EquipmentCapacity)
            {
                EquipmentUsed.Add(equipment);
            }
            else
            {
                Console.WriteLine("Cannot add more equipment, capacity reached.");
            }
        }
    }
}

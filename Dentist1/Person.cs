using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Person
    {
        private static List<Person> extent = new List<Person>();

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public Person(string firstName, string lastName, DateTime dateOfBirth, string gender, string address, int phoneNumber, string email, string middleName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            MiddleName = middleName;

            AddToExtent(this);
        }

        private static void AddToExtent(Person person)
        {
            if (person == null)
            {
                throw new ArgumentException("Person cannot be null");
            }
            extent.Add(person);
        }

        public static List<Person> GetExtent()
        {
            return new List<Person>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Person>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Person>();
            }
        }

        public static void DisplayAllPersons()
        {
            foreach (var person in extent)
            {
                Console.WriteLine($"Name: {person.FirstName} {person.MiddleName} {person.LastName}, Email: {person.Email}, Phone: {person.PhoneNumber}");
            }
        }
    }
}

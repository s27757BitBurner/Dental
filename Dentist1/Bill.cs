using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Bill
    {
        private static List<Bill> extent = new List<Bill>();

        public int IdBilling { get; set; }
        public int Amount { get; set; }
        public DateTime DateIssued { get; set; }
        public string Status { get; set; }
        public List<Payment> Payments { get; set; }

        public Bill(int idBilling, int amount, DateTime dateIssued, string status)
        {
            IdBilling = idBilling;
            Amount = amount;
            DateIssued = dateIssued;
            Status = status;
            Payments = new List<Payment>();

            AddToExtent(this);
        }

        private static void AddToExtent(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentException("Bill cannot be null");
            }
            extent.Add(bill);
        }

        public static List<Bill> GetExtent()
        {
            return new List<Bill>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Bill>>(json) ?? new List<Bill>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Bill>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Bill>();
            }
        }

        public static void DisplayAllBills()
        {
            foreach (var bill in extent)
            {
                Console.WriteLine($"Billing ID: {bill.IdBilling}, Amount: {bill.Amount}, Date Issued: {bill.DateIssued}, Status: {bill.Status}");
            }
        }

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
        }
    }
}

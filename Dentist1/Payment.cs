using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dentist1
{
    internal class Payment
    {
        private static List<Payment> extent = new List<Payment>();

        public int PaymentID { get; set; }
        public string Method { get; set; }
        public int InsuranceId { get; set; }
        public int PatientID { get; set; }
        public int InsuranceCoveredAmount { get; set; }
        public int PatientPaidAmount { get; set; }
        public Bill AssociatedBill { get; set; }

        public Payment(int paymentID, string method, int insuranceId, int patientID, int insuranceCoveredAmount, int patientPaidAmount, Bill associatedBill)
        {
            PaymentID = paymentID;
            Method = method;
            InsuranceId = insuranceId;
            PatientID = patientID;
            InsuranceCoveredAmount = insuranceCoveredAmount;
            PatientPaidAmount = patientPaidAmount;
            AssociatedBill = associatedBill;

            associatedBill.AddPayment(this);
            AddToExtent(this);
        }

        private static void AddToExtent(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentException("Payment cannot be null");
            }
            extent.Add(payment);
        }

        public static List<Payment> GetExtent()
        {
            return new List<Payment>(extent);
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
                    extent = JsonSerializer.Deserialize<List<Payment>>(json) ?? new List<Payment>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    extent = new List<Payment>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                extent = new List<Payment>();
            }
        }

        public static void DisplayAllPayments()
        {
            foreach (var payment in extent)
            {
                Console.WriteLine($"PaymentID: {payment.PaymentID}, Method: {payment.Method}, PatientID: {payment.PatientID}, Amount: {payment.PatientPaidAmount}");
            }
        }
    }
}

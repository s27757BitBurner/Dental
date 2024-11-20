using System;
using System.Collections.Generic;
using Dentist1;

class Program
{
    static void Main(string[] args)
    {
       
        Console.WriteLine($"Current Directory: {System.IO.Directory.GetCurrentDirectory()}");


        Patient.LoadExtent("patients.json");
        Room.LoadExtent("rooms.json"); 
        StaffMember.LoadExtent("staff.json");
        Appointment.LoadExtent("appointments.json");

       
        Patient patient = new Patient("John", "Doe", DateTime.Now.AddYears(-30), "Male", "123 Main St", 1234567890, "john@example.com", "P001", "InsuranceProvider", "Insurance123");
        Room room = new Room(101, 1);

        StaffMember staff1 = new StaffMember("Staff1", "Doe", DateTime.Now.AddYears(-40), "Female", "456 Another St", 1234567890, "staff1@example.com", "S001", DateTime.Now.AddYears(-5), 50000, "9-5", true, "Active", "Full");
        StaffMember staff2 = new StaffMember("Staff2", "Smith", DateTime.Now.AddYears(-35), "Female", "789 Other St", 1234567890, "staff2@example.com", "S002", DateTime.Now.AddYears(-3), 45000, "10-6", true, "Active", "Part");

        List<StaffMember> staffMembers = new List<StaffMember> { staff1, staff2 };

        Appointment appointment1 = new Appointment("A001", "Dental Checkup", DateTime.Now, TimeSpan.FromHours(1), "Scheduled", patient, staffMembers, room);
        Appointment appointment2 = new Appointment("A002", "Root Canal", DateTime.Now.AddDays(1), TimeSpan.FromHours(1.5), "Scheduled", patient, staffMembers, room);

       
        Console.WriteLine("Appointments before saving:");
        Appointment.DisplayAllAppointments();

     
        Patient.SaveExtent("patients.json");
        Room.SaveExtent("rooms.json");
        StaffMember.SaveExtent("staff.json");
        Appointment.SaveExtent("appointments.json");

       
        Appointment.LoadExtent("appointments.json");

        Console.WriteLine("\nAppointments after loading:");
        Appointment.DisplayAllAppointments();

        Console.WriteLine("Data saved and loaded successfully. Press any key to exit.");
        Console.ReadKey();
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dentist1
{
    internal class Appointment
    {


        private static List<Appointment> _extent = new List<Appointment>();

        public string AppointmentId { get; set; }
        public string ReasonForVisit { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public Patient ScheduledBy { get; set; }
        public List<StaffMember> AssistedBy { get; set; }
        public Room AppointmentRoom { get; set; }

        public Appointment() { }
        
        public Appointment(string appointmentID, string reasonForVisit, DateTime date, TimeSpan time, string status,
            Patient patient, List<StaffMember> staffMembers, Room room, string notes = null)
        {
            AppointmentId = appointmentID;
            ReasonForVisit = reasonForVisit;
            Date = date;
            Time = time;
            Status = status;
            ScheduledBy = patient;
            AssistedBy = staffMembers;
            AppointmentRoom = room;
            Notes = notes;

            AddToExtent(this);
        }

        private static void AddToExtent(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentException("Appointment cannot be null");
            }

            _extent.Add(appointment);
        }

        public static List<Appointment> GetExtent()
        {
            return new List<Appointment>(_extent);
        }

        public static void SaveExtent(string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
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
                    _extent = JsonSerializer.Deserialize<List<Appointment>>(json) ?? new List<Appointment>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading extent: {ex.Message}");
                    _extent = new List<Appointment>();
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing an empty extent.");
                _extent = new List<Appointment>();
            }
        }

        public static void DisplayAllAppointments()
        {
            foreach (var appointment in _extent)
            {
                Console.WriteLine(
                    $"AppointmentID: {appointment.AppointmentId}, Reason: {appointment.ReasonForVisit}, Date: {appointment.Date}, Status: {appointment.Status}");
            }
        }

        public void Change(string newReasonForVisit, DateTime newDate, TimeSpan newTime, string newStatus,
            string newNotes = null)
        {
            ReasonForVisit = newReasonForVisit;
            Date = newDate;
            Time = newTime;
            Status = newStatus;
            Notes = newNotes;
        }

        public void Cancel()
        {
            Status = "Cancelled";
        }
    }
}


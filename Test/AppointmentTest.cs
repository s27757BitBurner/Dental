using Dentist1;

namespace AppointmentTest
{
    [TestFixture]
    public class AppointmentTest
    {
        private Patient patient = new Patient("John Doe", "123456789");
        private List<StaffMember> staffMembers = new List<StaffMember> { new StaffMember("Dr. Smith"), new StaffMember("Nurse Jane") };
        private Room room = new Room("Room 101");

        private readonly Appointment appointment1;
        private readonly Appointment appointment2;

        public AppointmentTests()
        {
            appointment1 = new Appointment("A001", "Routine Checkup", DateTime.Now, new TimeSpan(10, 0, 0), "Scheduled", patient, staffMembers, room);
            appointment2 = new Appointment("A002", "Dental Cleaning", DateTime.Now.AddDays(1), new TimeSpan(11, 30, 0), "Scheduled", patient, staffMembers, room);
        }

        [Test]
        public void TestDisplayAppointmentDetails()
        {
            appointment1.DisplayAllAppointments();
        }

        [Test]
        public void TestChangeAppointmentDetails()
        {
            appointment1.Change("Emergency Visit", DateTime.Now.AddDays(2), new TimeSpan(9, 0, 0), "Scheduled", "Patient requested urgent care.");
            Assert.That(appointment1.ReasonForVisit, Is.EqualTo("Emergency Visit"));
            Assert.That(appointment1.Date, Is.EqualTo(DateTime.Now.AddDays(2)));
            Assert.That(appointment1.Time, Is.EqualTo(new TimeSpan(9, 0, 0)));
            Assert.That(appointment1.Status, Is.EqualTo("Scheduled"));
            Assert.That(appointment1.Notes, Is.EqualTo("Patient requested urgent care."));
        }

        [Test]
        public void TestCancelAppointment()
        {
            appointment1.Cancel();
            Assert.That(appointment1.Status, Is.EqualTo("Cancelled"));
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string filePath = "appointments.json";
            Appointment.SaveExtent(filePath);
            Appointment.LoadExtent(filePath);
            Assert.That(Appointment.Extent.Count, Is.GreaterThan(0));
        }
    }
}

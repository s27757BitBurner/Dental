using Dentist1;

namespace RoomTest
{
    [TestFixture]
    public class RoomTest
    {
        private readonly Room room1 = new Room(101, 1);
        private readonly Room room2 = new Room(102, 2);

        [Test]
        public void TestRoomInitialization()
        {
            Assert.That(room1.Number, Is.EqualTo(101));
            Assert.That(room1.Floor, Is.EqualTo(1));
            Assert.That(room1.EquipmentUsed.Count, Is.EqualTo(0)); // Check that the list is empty initially
        }

        [Test]
        public void TestAddEquipmentWithinCapacity()
        {
            var equipment = new Equipment("X-Ray Machine", "Diagnostic");
            room1.AddEquipment(equipment);
            Assert.That(room1.EquipmentUsed.Count, Is.EqualTo(1));
            Assert.That(room1.EquipmentUsed[0].Name, Is.EqualTo("X-Ray Machine"));
        }

        [Test]
        public void TestAddEquipmentExceedingCapacity()
        {
            room1.EquipmentCapacity = 1; // Set capacity to 1 for this test
            var equipment1 = new Equipment("X-Ray Machine", "Diagnostic");
            var equipment2 = new Equipment("Dental Chair", "Treatment");

            room1.AddEquipment(equipment1);
            room1.AddEquipment(equipment2); // This should not be added

            Assert.That(room1.EquipmentUsed.Count, Is.EqualTo(1));
            Assert.That(room1.EquipmentUsed[0].Name, Is.EqualTo("X-Ray Machine"));
        }
    }
}

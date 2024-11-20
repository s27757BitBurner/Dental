using Dentist1;

namespace DentalTreatmentTest
{
    [TestFixture]
    public class DentalTreatmentTest
    {
        private readonly DentalTreatment treatment1 = new DentalTreatment(
            "P001", "Basic", 0.8,
            "T001", "Root Canal", 1500, "Treatment to save a tooth.",
            "2 hours", 2.0, DateTime.Now, new TimeSpan(10, 0, 0)
        );

        private readonly DentalTreatment treatment2 = new DentalTreatment(
            "P002", "Premium", 0.9,
            "T002", "Dental Implants", 3000, "Replacement of missing teeth.",
            "3 hours", 3.0, DateTime.Now.AddDays(1), new TimeSpan(14, 30, 0)
        );

        [Test]
        public void TestDentalTreatmentInitialization()
        {
            Assert.That(treatment1.PlanID, Is.EqualTo("P001"));
            Assert.That(treatment1.TreatmentName, Is.EqualTo("Root Canal"));
            Assert.That(treatment1.Cost, Is.EqualTo(1500));
            Assert.That(treatment1.Duration, Is.EqualTo(2.0));
            Assert.That(treatment1.GeneratedBill.Status, Is.EqualTo("Pending"));
            Assert.That(treatment1.RequiredMedications.Count, Is.EqualTo(0)); // Check that the list is empty
        }

        [Test]
        public void TestAddMedication()
        {
            var medication = new Medication("M001", "Pain Reliever", 20);
            treatment1.AddMedication(medication);
            Assert.That(treatment1.RequiredMedications.Count, Is.EqualTo(1));
            Assert.That(treatment1.RequiredMedications[0].MedicationName, Is.EqualTo("Pain Reliever"));
        }

        [Test]
        public void TestGeneratedBillOnCreation()
        {
            Assert.That(treatment2.GeneratedBill, Is.Not.Null);
            Assert.That(treatment2.GeneratedBill.Amount, Is.EqualTo(3000));
            Assert.That(treatment2.GeneratedBill.DateIssued, Is.Not.Null);
            Assert.That(treatment2.GeneratedBill.Status, Is.EqualTo("Pending"));
        }
    }
}

using Dentist1;

namespace BillTest
{
    [TestFixture]
    public class BillTest
    {
        private readonly Bill bill1 = new Bill(1, 100, DateTime.Now, "Pending");
        private readonly Bill bill2 = new Bill(2, 200, DateTime.Now, "Paid");

        [Test]
        public void TestBillInitialization()
        {
            Assert.That(bill1.IdBilling, Is.EqualTo(1));
            Assert.That(bill1.Amount, Is.EqualTo(100));
            Assert.That(bill1.DateIssued, Is.Not.Null);
            Assert.That(bill1.Status, Is.EqualTo("Pending"));
            Assert.That(bill1.Payments.Count, Is.EqualTo(0); 

        [Test]
        public void TestAddPayment()
        {
            var payment = new Payment(1, 50, DateTime.Now, "Paid");
            bill1.AddPayment(payment);
            Assert.That(bill1.Payments.Count, Is.EqualTo(1));
            Assert.That(bill1.Payments[0].Amount, Is.EqualTo(50));
        }

        [Test]
        public void TestMultiplePayments()
        {
            var payment1 = new Payment(1, 70, DateTime.Now, "Paid");
            var payment2 = new Payment(2, 30, DateTime.Now, "Paid");

            bill2.AddPayment(payment1);
            bill2.AddPayment(payment2);

            Assert.That(bill2.Payments.Count, Is.EqualTo(2));
            Assert.That(bill2.Payments[0].Amount, Is.EqualTo(70));
            Assert.That(bill2.Payments[1].Amount, Is.EqualTo(30));
        }
    }
}

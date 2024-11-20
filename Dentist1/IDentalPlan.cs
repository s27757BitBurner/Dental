namespace Dentist1
{
    internal interface IDentalPlan
    {
        string PlanID { get; set; }
        string PlanType { get; set; }
        double Coverage { get; set; }
    }
}

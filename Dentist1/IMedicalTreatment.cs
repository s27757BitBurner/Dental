namespace Dentist1
{
    internal interface IMedicalTreatment
    {
        string TreatmentID { get; set; }
        string TreatmentName { get; set; }
        int Cost { get; set; }
        string Description { get; set; }
        string AverageDuration { get; set; }
    }
}

namespace BikeRentalApplication.Entities
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
    }
    public enum EmailTemplate
    {
        Registration,  
        RentalConfirmation,
        RentalOverDue
    }

    public enum EmailBody
    {

    }
}

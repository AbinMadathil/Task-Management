namespace TaskMgmt.Services.ConfigurationClass
{
    public class EmailConfiguration
    {
        public string SenderEmail { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string SenderPassword { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
    }

}

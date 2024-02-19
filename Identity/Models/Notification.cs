namespace Identity.Models
{
    public class Notification
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NotificationType { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string From { get; set; } = "";
        public string To { get; set; } = "";
    }
}

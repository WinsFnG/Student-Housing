namespace NexusThuisWeb.Models
{
    public class Events
    {
        public int id { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }

        public DateTime event_date { get; set; }

        public int? created_by { get; set; }

        public int? vcode { get; set; }
    }
}

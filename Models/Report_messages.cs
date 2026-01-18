namespace NexusThuisWeb.Models
{
    public class Report_messages
    {
        public int id { get; set; }

        public int sender {  get; set; }

        public int receiver { get; set; }

        public string? message { get; set; }

        public DateTime created_at { get; set; }

        public int vcode { get; set; }
    }
}

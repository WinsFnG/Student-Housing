namespace NexusThuisWeb.Models
{
    public class Reports
    {
        public int id { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }


        public string? photo_url { get; set; }

        public int vcode { get; set; }

        public int created_by { get; set; }

        public DateTime created_at { get; set; }
    }
}

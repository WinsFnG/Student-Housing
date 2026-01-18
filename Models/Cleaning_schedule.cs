namespace NexusThuisWeb.Models
{
    public class Cleaning_schedule
    {
        public int id { get; set; }   

        public DateTime date { get; set; }

        public string? weekday { get; set; }

        public string? kitchen {  get; set; }

        public string? living_room { get; set; }

        public string? bathroom { get; set; }

        public string? storage {  get; set; }

        public string? name { get; set; }

        public int? vcode { get; set; }
    }
}

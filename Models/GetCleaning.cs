namespace NexusThuisWeb.Models
{
    public class GetCleaning
    {
        public int id { get; set; }
        public DateTime date {  get; set; }

        public string? bathroom { get; set; }

        public string? kitchen { get; set; }

        public string? livingroom { get; set; }

        public string? storageroom { get; set; }


    }
}

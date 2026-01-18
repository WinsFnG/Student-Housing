namespace NexusThuisWeb.Models
{
    public class Shared_chats
    {
        public int id { get; set; }

        public int vcode { get; set; }

        public int sender {  get; set; }

        public int receiver { get; set; }

        public string? message { get; set; }

        public DateTime created_at { get; set; }

    }
}

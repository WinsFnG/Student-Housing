namespace NexusThuisWeb.Models
{
    public class Profiles
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? password { get; set; }

        public string? email { get; set; }

        public int is_landlord { get; set; } = 0;

        public bool banned { get; set; }

        public bool accepted_terms { get; set; }

        public string? profile_picture { get; set; }

        public int vcode { get; set; }


    }
}

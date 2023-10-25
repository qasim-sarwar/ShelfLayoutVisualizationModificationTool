using System.Text.Json.Serialization;

namespace TexCode.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public  string? Role { get; set; }
        public DateTime Created { get; set; }
        public  string? VerificationToken { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
    }
}

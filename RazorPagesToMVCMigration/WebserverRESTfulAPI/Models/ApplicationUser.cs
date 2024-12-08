namespace ServiceAPI.Models
{
    public class ApplicationUser
    {
        public required string Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
    }
}

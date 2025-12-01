namespace MyWebApp.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public bool IsOnline { get; set; }

    }
}

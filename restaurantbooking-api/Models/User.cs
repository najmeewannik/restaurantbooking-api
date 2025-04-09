namespace restaurantbooking_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = "User";
    }
}

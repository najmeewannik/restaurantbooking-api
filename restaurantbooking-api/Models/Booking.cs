namespace restaurantbooking_api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public int TableId { get; set; }
        public Table? Table { get; set; }

        public DateTime BookingTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

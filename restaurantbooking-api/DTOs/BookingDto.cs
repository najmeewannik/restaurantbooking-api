namespace restaurantbooking_api.DTOs
{
    public class BookingDto
    {
        public int UserId { get; set; }
        public int TableId { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

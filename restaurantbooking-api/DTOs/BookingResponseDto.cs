namespace restaurantbooking_api.DTOs
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int TableId { get; set; }
        public string? TableName { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

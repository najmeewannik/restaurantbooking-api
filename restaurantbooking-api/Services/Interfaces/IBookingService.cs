using restaurantbooking_api.DTOs;

namespace restaurantbooking_api.Services.Interfaces
{
    public interface IBookingService
    {
        Task<bool> IsTimeSlotAvailableAsync(int tableId, DateTime start, DateTime end);
        Task<BookingResponseDto?> CreateBookingAsync(BookingDto dto);
        Task<BookingResponseDto?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
        Task<bool> UpdateBookingAsync(int id, BookingDto dto);
        Task<bool> DeleteBookingAsync(int id);
    }
}

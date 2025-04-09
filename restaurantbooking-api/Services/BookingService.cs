using restaurantbooking_api.Data;
using restaurantbooking_api.Models;
using restaurantbooking_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using restaurantbooking_api.DTOs;

namespace restaurantbooking_api.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int tableId, DateTime start, DateTime end)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.TableId == tableId &&
                (
                    (b.BookingTime <= start && b.EndTime > start) ||  // เริ่มทับของเดิม
                    (b.BookingTime < end && b.EndTime >= end) ||      // จบทับของเดิม
                    (b.BookingTime >= start && b.EndTime <= end)      // ครอบช่วงเดิม
                )
            );
        }

        public async Task<BookingResponseDto?> CreateBookingAsync(BookingDto dto)
        {
            var isAvailable = await IsTimeSlotAvailableAsync(dto.TableId, dto.BookingTime, dto.EndTime);
            if (!isAvailable)
                return null;

            var booking = new Booking
            {
                UserId = dto.UserId,
                TableId = dto.TableId,
                BookingTime = dto.BookingTime,
                EndTime = dto.EndTime
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return await GetBookingByIdAsync(booking.Id);
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.Id == id);

            return booking == null ? null : MapToResponseDto(booking);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .ToListAsync();

            return bookings.Select(MapToResponseDto);
        }

        public async Task<bool> UpdateBookingAsync(int id, BookingDto dto)
        {
            var existing = await _context.Bookings.FindAsync(id);
            if (existing == null)
                return false;

            bool isAvailable = await IsTimeSlotAvailableAsync(dto.TableId, dto.BookingTime, dto.EndTime);
            if (!isAvailable)
                return false;

            existing.TableId = dto.TableId;
            existing.BookingTime = dto.BookingTime;
            existing.EndTime = dto.EndTime;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        private BookingResponseDto MapToResponseDto(Booking booking)
        {
            return new BookingResponseDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                TableId = booking.TableId,
                BookingTime = booking.BookingTime,
                EndTime = booking.EndTime,
                Username = booking.User?.Username ?? "",
                TableName = booking.Table?.Name ?? ""
            };
        }
    }
}

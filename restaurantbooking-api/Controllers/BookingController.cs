using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantbooking_api.Data;
using restaurantbooking_api.DTOs;
using restaurantbooking_api.Models;
using restaurantbooking_api.Services.Interfaces;

namespace restaurantbooking_api.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] BookingDto dto)
        {
            var result = await _bookingService.CreateBookingAsync(dto);

            if (result == null)
            {
                return Conflict("The selected table is already booked during the selected time.");
            }

            return CreatedAtAction(nameof(GetBooking), new { id = result.Id }, result);
        }

        // GET: api/bookings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponseDto>> GetBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // PUT: api/bookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDto dto)
        {
            var success = await _bookingService.UpdateBookingAsync(id, dto);
            if (!success)
            {
                return Conflict("This time slot is already booked or booking not found.");
            }

            return Ok("Update Booking Complete");
        }

        // DELETE: api/bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);
            if (!success)
                return NotFound();

            return Ok("Delete Booking!");
        }
    }
}

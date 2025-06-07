using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CertBE.Models;
using CertBE.DTOs;

namespace CertBE.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly CertificationContext _context;

        public BookingController(CertificationContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            return await _context.Bookings
                .Include(booking => booking.Customer)
                .Include(booking => booking.ExamDate)
                .ThenInclude(examDate => examDate.Cert)
                .ThenInclude(cert => cert.Category)            
                .Select(booking => new BookingDTO()
                    {
                        Category = booking.ExamDate.Cert.Category.Name,
                        CertName = booking.ExamDate.Cert.Name,
                        Price = (float)booking.ExamDate.Cert.Price,
                        ExamStartingTime = booking.ExamDate.Startt,
                        ExamEndingTime = booking.ExamDate.Endd,
                        CustomerFirstName = booking.Customer.FirstName,
                        CustomerLastName = booking.Customer.LastName
                    })
                    .ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingPostDTO bookingPostDTO)
        {
            var examDate = await _context.ExamDates.FindAsync(bookingPostDTO.ExamId);

            if (examDate == null)
            {
                return NotFound();
            }
            if (examDate.Startt != bookingPostDTO.ExamStartingTime || examDate.Endd != bookingPostDTO.ExamEndingTime)
            {
                return BadRequest("Exam id doesn't match given date.");
            }

            var customer = new Customer()
            {
                FirstName = bookingPostDTO.CustomerFirstName,
                LastName = bookingPostDTO.CustomerLastName,
                Email = bookingPostDTO.CustomerEmail,
                Password = bookingPostDTO.CustomerPassword
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var booking = new Booking();
            booking.CustomerId = customer.Id;
            booking.Customer = customer;
            booking.ExamDateId = examDate.Id;
            booking.ExamDate = examDate;            

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}

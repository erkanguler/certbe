using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CertBE.Models;
using CertBE.DTOs;

namespace CertBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamDateController : ControllerBase
    {
        private readonly CertificationContext _context;

        public ExamDateController(CertificationContext context)
        {
            _context = context;
        }

        // GET: api/ExamDate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDateDTO>>> GetExamDates()
        {
            return await _context.ExamDates
                .Include(ed => ed.Cert)
                .ThenInclude(cert => cert.Category)
                .Select(ed => new ExamDateDTO()
                                    {
                                        Id = ed.Id,
                                        Category = ed.Cert.Category.Name,
                                        CertName = ed.Cert.Name,
                                        Price = (float)ed.Cert.Price,
                                        ExamStartingTime = ed.Startt,
                                        ExamEndingTime = ed.Endd
                                    }).ToListAsync();
        }

        // GET: api/ExamDate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDate>> GetExamDate(int id)
        {
            var examDate = await _context.ExamDates.FindAsync(id);

            if (examDate == null)
            {
                return NotFound();
            }

            return examDate;
        }

        // PUT: api/ExamDate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamDate(int id, ExamDatePutDTO examDatePutDTO)
        {
            if (id != examDatePutDTO.Id)
            {
                return BadRequest();
            }

            var examDate = await _context.ExamDates.FindAsync(id);

            if (examDate == null)
            {
                return NotFound();
            }

            var start = examDatePutDTO.ExamStartingTime;
            var end = examDatePutDTO.ExamEndingTime;

            if (-1 < DateTime.Compare(start, end))
            {
                return BadRequest("Exam ending time can't be before starting time.");
            }

            examDate.Startt = start;
            examDate.Endd = end;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/ExamDate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamDate>> PostExamDate(ExamDatePostDTO examDatePostDTO)
        {
            var cert = await _context.Certs.FindAsync(examDatePostDTO.CertId);

            if (cert == null)
            {
                return NotFound();
            }

            var examDate = new ExamDate();
            examDate.Cert = cert;
            examDate.Startt = examDatePostDTO.ExamStartingTime;
            examDate.Endd = examDatePostDTO.ExamEndingTime;

            _context.ExamDates.Add(examDate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamDate", new { id = examDate.Id }, examDate);
        }

        // DELETE: api/ExamDate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamDate(int id)
        {
            var examDate = await _context.ExamDates.FindAsync(id);
            if (examDate == null)
            {
                return NotFound();
            }

            _context.ExamDates.Remove(examDate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamDateExists(int id)
        {
            return _context.ExamDates.Any(e => e.Id == id);
        }
    }
}

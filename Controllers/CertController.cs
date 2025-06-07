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
    public class CertController : ControllerBase
    {
        private readonly CertificationContext _context;

        public CertController(CertificationContext context)
        {
            _context = context;
        }

        // GET: api/Cert
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertGetDTO>>> GetCerts()
        {
            return await _context.Certs
                .Include(cert => cert.Category)
                .Select(cert => new CertGetDTO()
                                    {
                                        Id = cert.Id,
                                        Category = cert.Category.Name,
                                        CertName = cert.Name,
                                        Price = (float)cert.Price
                                    })
                                    .ToListAsync();
        }

        // GET: api/Cert/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cert>> GetCert(int id)
        {
            var cert = await _context.Certs.FindAsync(id);

            if (cert == null)
            {
                return NotFound();
            }

            return cert;
        }

        // PUT: api/Cert/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCert(int id, CertPutDTO certPutDTO)
        {
            if (id != certPutDTO.Id)
            {
                return BadRequest();
            }

            var cert = await _context.Certs.FindAsync(id);

            if (cert == null)
            {
                return NotFound();
            }

            cert.CategoryId = certPutDTO.CategoryId;
            cert.Name = certPutDTO.CertName;
            cert.Price = (decimal)certPutDTO.Price;

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

        // POST: api/Cert
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cert>> PostCert(CertPostDTO certDTO)
        {
            var category = await _context.Categories.FindAsync(certDTO.CategoryId);

            if (category == null)
            {
                return NotFound();
            }

            var cert = new Cert();
            cert.CategoryId = certDTO.CategoryId;
            cert.Category = category;
            cert.Name = certDTO.CertName;
            cert.Price = (Decimal)certDTO.Price;

            _context.Certs.Add(cert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCert", new { id = cert.Id }, cert);
        }

        // DELETE: api/Cert/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCert(int id)
        {
            var cert = await _context.Certs.FindAsync(id);
            if (cert == null)
            {
                return NotFound();
            }

            _context.Certs.Remove(cert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertExists(int id)
        {
            return _context.Certs.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerInspiration.Models;

namespace ServerInspiration.Controllers
{
    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class FevoritesController : ControllerBase
    {
        private readonly InspirationDBContext _context;

        public FevoritesController(InspirationDBContext context)
        {
            _context = context;
        }

        // GET: api/Fevorites
        [HttpGet]
        public IEnumerable<Fevorite> GetFevorites()
        {
            return _context.Fevorites;
        }

        // GET: api/Fevorites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFevorite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fevorite = await _context.Fevorites.FindAsync(id);

            if (fevorite == null)
            {
                return NotFound();
            }

            return Ok(fevorite);
        }

        // PUT: api/Fevorites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFevorite([FromRoute] int id, [FromBody] Fevorite fevorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fevorite.SongID)
            {
                return BadRequest();
            }

            _context.Entry(fevorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FevoriteExists(id))
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

        // POST: api/Fevorites
        [HttpPost]
        public async Task<IActionResult> PostFevorite([FromBody] Fevorite fevorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fevorites.Add(fevorite);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FevoriteExists(fevorite.SongID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFevorite", new { id = fevorite.SongID }, fevorite);
        }

        // DELETE: api/Fevorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFevorite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fevorite = await _context.Fevorites.FindAsync(id);
            if (fevorite == null)
            {
                return NotFound();
            }

            _context.Fevorites.Remove(fevorite);
            await _context.SaveChangesAsync();

            return Ok(fevorite);
        }

        // DELETE: api/Fevorites/5
        [HttpDelete("{id}-{id2}")]
        public async Task<IActionResult> DeleteFevorite([FromRoute] int id, [FromRoute] int id2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fevorite = await _context.Fevorites.FindAsync(id, id2);
            if (fevorite == null)
            {
                return NotFound();
            }

            _context.Fevorites.Remove(fevorite);
            await _context.SaveChangesAsync();

            return Ok(fevorite);
        }

        private bool FevoriteExists(int id)
        {
            return _context.Fevorites.Any(e => e.SongID == id);
        }
    }
}
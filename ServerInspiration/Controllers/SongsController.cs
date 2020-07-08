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
    public class SongsController : ControllerBase
    {
        private readonly InspirationDBContext _context;

        public SongsController(InspirationDBContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public IEnumerable<Song> GetSongs()
        {
            IEnumerable<Song> songs = _context.Songs.Include(c => c.User);
            return songs;
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Songs.FindAsync(id);
            User user = await _context.Users.Where(u => u.UserID == song.User.UserID).FirstOrDefaultAsync();
            song.User = user;

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong([FromRoute] int id, [FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.SongID)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        [HttpPost]
        public async Task<IActionResult> PostSong([FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //User user = await _context.Users.Where(u => u.UserName == song.User.UserName).FirstOrDefaultAsync();
            //song.User.UserID = user.UserID;

            User user = await _context.Users.Where(u => u.UserName == song.User.UserName).FirstOrDefaultAsync();
            song.User.UserID = user.UserID;

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.SongID }, song);
        }

        // POST: api/Songs/Fevorite
        [HttpPost("Fevorite")]
        public IEnumerable<Song> PostSong([FromBody] User user)
        {
            User newUser = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            IEnumerable<Fevorite> fevorite = _context.Fevorites.Where(x => x.UserID == newUser.UserID);

            IEnumerable<Song> songs = _context.Songs.Include(c => c.User);

            foreach (var s in songs)
            {
                if (fevorite.Any(x => x.SongID == s.SongID && x.UserID == newUser.UserID)) s.Fevorite = true;
                else s.Fevorite = false;
            }

            return songs;
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return Ok(song);
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }
    }
}
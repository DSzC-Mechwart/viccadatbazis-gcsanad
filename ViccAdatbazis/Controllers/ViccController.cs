using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        //Adatbázis kapcsolat
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }
        //Összes vicc lekérdezése
        [HttpGet]
        public ActionResult<List<Vicc>> GetViccek()
        {
            return _context.Viccek.Where(x => x.Aktiv == true).ToList();
        }

        public async Task<ActionResult<List<Vicc>>> GetAsyncViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv == true).ToListAsync();
        }

        //Egy Vicc lekérdezése
        [HttpGet("{id}")]

        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            //if (vicc == null)
            //{
            //    return NotFound();
            //}
            return vicc == null ? NotFound() : vicc;
        }
        //Vicc feltöltése
        [HttpPost]
        public async Task<ActionResult<Vicc>> PostVicc(Vicc vicc)
        {
            _context.Viccek.Add(vicc);
            await _context.SaveChangesAsync();

            //A válasz maga a vicc 
            return CreatedAtAction("GetVicc", new { id = vicc.Id }, vicc);
        }

        //Vicc módosítása
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc vicc)
        {
            if (id != vicc.Id)
            {
                return BadRequest();
            }
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        //Vicc törlése
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null)
            {
                return NotFound();
            }
            if (vicc.Aktiv)
            {
                vicc.Aktiv = false;
            }
            else
            {
                _context.Viccek.Remove(vicc);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Lájkolás
        [Route("{id}/like")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> Tetszik(int id)
        {
            var vicc = _context.Viccek.Find(id);
            if (vicc == null) { return NotFound(); }
            vicc.Tetszik++;
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(JsonSerializer.Serialize("tdb: "+vicc.Tetszik));
        }

        //Dislike
        [Route("{id}/dislike")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> NemTetszik(int id)
        {
            var vicc = _context.Viccek.Find(id);
            if (vicc == null) { return NotFound(); }
            vicc.NemTetszik++;
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using KrugerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrugerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly KrugerAppContext _context;

        public ParkingsController(KrugerAppContext context)
        {
            _context = context;
        }

        // GET: api/Parkings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetMParking()
        {
            return await _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner)
                .ToListAsync();
        }

        // GET: api/Parkings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parking>> GetParking(long id)
        {
            var parking = await _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        // PUT: api/Parkings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(long id, Parking parking)
        {
            if (id != parking.Id)
            {
                return BadRequest();
            }

            _context.Entry(parking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        // POST: api/Parkings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(Parking parking)
        {
            _context.MParking.Add(parking);
            await _context.SaveChangesAsync();

            var item = await _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(i => i.Id == parking.Id);

            return CreatedAtAction("GetParking", new { id = parking.Id }, item);
        }

        // DELETE: api/Parkings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(long id)
        {
            var parking = await _context.MParking.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }

            _context.MParking.Remove(parking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingExists(long id)
        {
            return _context.MParking.Any(e => e.Id == id);
        }
    }
}

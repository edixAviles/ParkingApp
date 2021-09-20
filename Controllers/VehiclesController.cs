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
    public class VehiclesController : ControllerBase
    {
        private readonly KrugerAppContext _context;

        public VehiclesController(KrugerAppContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetMVehicle()
        {
            return await _context.MVehicle
                .Include(i => i.Type)
                .Include(i => i.Owner)
                .ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(long id)
        {
            var vehicle = await _context.MVehicle
                .Include(i => i.Type)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(long id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.MVehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            var item = await _context.MVehicle
                .Include(i => i.Type)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(i => i.Id == vehicle.Id);

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, item);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            var vehicle = await _context.MVehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.MVehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(long id)
        {
            return _context.MVehicle.Any(e => e.Id == id);
        }
    }
}

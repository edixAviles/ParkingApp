using KrugerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KrugerApp.Views
{
    public class ParkingsController : Controller
    {
        private readonly KrugerAppContext _context;

        public ParkingsController(KrugerAppContext context)
        {
            _context = context;
        }

        // GET: Parkings
        public async Task<IActionResult> Index()
        {
            var krugerAppContext = _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner);
            return View(await krugerAppContext.ToListAsync());
        }

        // GET: Parkings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // GET: Parkings/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.MVehicle, "Id", "Mark");
            return View();
        }

        // POST: Parkings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntryDate,ExitDate,ParkingValue,Observation,VehicleId")] Parking parking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["VehicleId"] = new SelectList(_context.MVehicle, "Id", "Mark", parking.VehicleId);


            return View(parking);
        }

        // GET: Parkings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.MParking.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.MVehicle, "Id", "Mark", parking.VehicleId);
            return View(parking);
        }

        // POST: Parkings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EntryDate,ExitDate,ParkingValue,Observation,VehicleId")] Parking parking)
        {
            if (id != parking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (parking.ExitDate != null && parking.ExitDate.Value > parking.EntryDate)
                    {
                        var vehicle = await _context.MVehicle
                            .Include(i => i.Type)
                            .FirstOrDefaultAsync(i => i.Id == id);

                        var minutes = (decimal)(parking.ExitDate.Value - parking.EntryDate).TotalMinutes;
                        var hours = Math.Ceiling(minutes / 60);
                        var total = vehicle.Type.ValuePerHour * hours;
                        parking.ParkingValue = total;
                    }


                    _context.Update(parking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingExists(parking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.MVehicle, "Id", "Mark", parking.VehicleId);
            return View(parking);
        }

        // GET: Parkings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.MParking
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Type)
                .Include(i => i.VehicleParked)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // POST: Parkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parking = await _context.MParking.FindAsync(id);
            _context.MParking.Remove(parking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingExists(long id)
        {
            return _context.MParking.Any(e => e.Id == id);
        }
    }
}

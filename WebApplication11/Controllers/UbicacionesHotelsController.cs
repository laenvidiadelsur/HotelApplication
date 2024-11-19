using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class UbicacionesHotelsController : Controller
    {
        private readonly MiContexto _context;

        public UbicacionesHotelsController(MiContexto context)
        {
            _context = context;
        }

        // GET: UbicacionesHotels
        public async Task<IActionResult> Index()
        {
            return View(await _context.UbicacionesHotels.ToListAsync());
        }

        // GET: UbicacionesHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionesHotel = await _context.UbicacionesHotels
                .FirstOrDefaultAsync(m => m.UbicacionId == id);
            if (ubicacionesHotel == null)
            {
                return NotFound();
            }

            return View(ubicacionesHotel);
        }

        // GET: UbicacionesHotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UbicacionesHotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UbicacionId,Direccion,Ciudad,Pais")] UbicacionesHotel ubicacionesHotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacionesHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacionesHotel);
        }

        // GET: UbicacionesHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionesHotel = await _context.UbicacionesHotels.FindAsync(id);
            if (ubicacionesHotel == null)
            {
                return NotFound();
            }
            return View(ubicacionesHotel);
        }

        // POST: UbicacionesHotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UbicacionId,Direccion,Ciudad,Pais")] UbicacionesHotel ubicacionesHotel)
        {
            if (id != ubicacionesHotel.UbicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacionesHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionesHotelExists(ubicacionesHotel.UbicacionId))
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
            return View(ubicacionesHotel);
        }

        // GET: UbicacionesHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacionesHotel = await _context.UbicacionesHotels
                .FirstOrDefaultAsync(m => m.UbicacionId == id);
            if (ubicacionesHotel == null)
            {
                return NotFound();
            }

            return View(ubicacionesHotel);
        }

        // POST: UbicacionesHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ubicacionesHotel = await _context.UbicacionesHotels.FindAsync(id);
            if (ubicacionesHotel != null)
            {
                _context.UbicacionesHotels.Remove(ubicacionesHotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionesHotelExists(int id)
        {
            return _context.UbicacionesHotels.Any(e => e.UbicacionId == id);
        }
    }
}

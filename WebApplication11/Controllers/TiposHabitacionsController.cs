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
    public class TiposHabitacionsController : Controller
    {
        private readonly MiContexto _context;

        public TiposHabitacionsController(MiContexto context)
        {
            _context = context;
        }

        // GET: TiposHabitacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposHabitacions.ToListAsync());
        }

        // GET: TiposHabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposHabitacion = await _context.TiposHabitacions
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tiposHabitacion == null)
            {
                return NotFound();
            }

            return View(tiposHabitacion);
        }

        // GET: TiposHabitacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoId,NombreTipo,Descripcion,PrecioBase")] TiposHabitacion tiposHabitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposHabitacion);
        }

        // GET: TiposHabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposHabitacion = await _context.TiposHabitacions.FindAsync(id);
            if (tiposHabitacion == null)
            {
                return NotFound();
            }
            return View(tiposHabitacion);
        }

        // POST: TiposHabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoId,NombreTipo,Descripcion,PrecioBase")] TiposHabitacion tiposHabitacion)
        {
            if (id != tiposHabitacion.TipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposHabitacionExists(tiposHabitacion.TipoId))
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
            return View(tiposHabitacion);
        }

        // GET: TiposHabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposHabitacion = await _context.TiposHabitacions
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tiposHabitacion == null)
            {
                return NotFound();
            }

            return View(tiposHabitacion);
        }

        // POST: TiposHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposHabitacion = await _context.TiposHabitacions.FindAsync(id);
            if (tiposHabitacion != null)
            {
                _context.TiposHabitacions.Remove(tiposHabitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposHabitacionExists(int id)
        {
            return _context.TiposHabitacions.Any(e => e.TipoId == id);
        }
    }
}

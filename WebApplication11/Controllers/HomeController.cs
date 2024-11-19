using Microsoft.AspNetCore.Mvc;
using WebApplication11.Data;
using WebApplication11.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApplication11.Controllers
{
    public class HomeController : Controller
    {
        private readonly MiContexto _context;

        public HomeController(MiContexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Consultar las habitaciones disponibles y sus datos relacionados
            var habitacionesDisponibles = _context.Habitaciones
                .Include(h => h.Tipo)
                .Include(h => h.Servicios)
                .Where(h => h.Estado == "disponible")
                .Select(h => new HabitacionViewModel
                {
                    HabitacionId = h.HabitacionId,
                    NumeroHabitacion = h.NumeroHabitacion,
                    Estado = h.Estado,
                    TipoNombre = h.Tipo.NombreTipo,
                    TipoDescripcion = h.Tipo.Descripcion,
                    PrecioBase = h.Tipo.PrecioBase,
                    Servicios = h.Servicios.Select(s => s.NombreServicio).ToList()
                })
                .ToList();

            var viewModel = new HabitacionViewModel
            {
                HabitacionesDisponibles = habitacionesDisponibles
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Reservar(HabitacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Cambiar el estado de la habitación a "reservada"
                var habitacion = _context.Habitaciones.Find(model.HabitacionId);
                if (habitacion != null && habitacion.Estado == "disponible")
                {
                    habitacion.Estado = "reservada";

                    // Crear la nueva reserva
                    var nuevaReserva = new Reserva
                    {
                        HabitacionId = model.HabitacionId,
                        FechaCheckIn = model.FechaCheckIn.GetValueOrDefault(),
                        FechaCheckOut = model.FechaCheckOut.GetValueOrDefault(),
                        Estado = "reservada",
                        CreadoEn = DateTime.Now
                    };

                    _context.Reservas.Add(nuevaReserva);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // Recargar las habitaciones disponibles si ocurre un error
            model.HabitacionesDisponibles = _context.Habitaciones
                .Where(h => h.Estado == "disponible")
                .Select(h => new HabitacionViewModel
                {
                    HabitacionId = h.HabitacionId,
                    NumeroHabitacion = h.NumeroHabitacion
                })
                .ToList();

            return View("Index", model);
        }
    }
}

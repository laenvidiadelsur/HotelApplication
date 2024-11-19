using System;
using System.Collections.Generic;

namespace WebApplication11.Models
{
    public class HabitacionViewModel
    {
        // Propiedades para la visualización de las habitaciones
        public int HabitacionId { get; set; }
        public string NumeroHabitacion { get; set; }
        public string Estado { get; set; }
        public string TipoNombre { get; set; }
        public string TipoDescripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public List<string> Servicios { get; set; } = new List<string>();

        // Propiedades para la reserva de habitaciones
        public DateOnly? FechaCheckIn { get; set; }
        public DateOnly? FechaCheckOut { get; set; }

        // Lista para manejar habitaciones disponibles en la vista
        public List<HabitacionViewModel> HabitacionesDisponibles { get; set; } = new List<HabitacionViewModel>();
    }
}

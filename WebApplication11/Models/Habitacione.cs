using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Habitacione
{
    public int HabitacionId { get; set; }

    public string NumeroHabitacion { get; set; } = null!;

    public int? TipoId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();

    public virtual TiposHabitacion? Tipo { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}

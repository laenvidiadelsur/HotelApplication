using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int? UsuarioId { get; set; }

    public int? HabitacionId { get; set; }

    public DateOnly FechaCheckIn { get; set; }

    public DateOnly FechaCheckOut { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    public virtual Habitacione? Habitacion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Usuario? Usuario { get; set; }
}

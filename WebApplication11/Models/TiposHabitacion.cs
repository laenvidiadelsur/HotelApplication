using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class TiposHabitacion
{
    public int TipoId { get; set; }

    public string NombreTipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioBase { get; set; }

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();
}

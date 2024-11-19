using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Habitacione> Habitacions { get; set; } = new List<Habitacione>();
}

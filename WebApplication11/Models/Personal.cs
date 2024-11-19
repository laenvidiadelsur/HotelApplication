using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Puesto { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }
}

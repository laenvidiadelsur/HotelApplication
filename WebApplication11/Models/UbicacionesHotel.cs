using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class UbicacionesHotel
{
    public int UbicacionId { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }
}

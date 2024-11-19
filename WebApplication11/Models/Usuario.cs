using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}

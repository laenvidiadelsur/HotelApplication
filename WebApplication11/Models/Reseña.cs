using System;
using System.Collections.Generic;

namespace WebApplication11.Models;

public partial class Reseña
{
    public int ReseñaId { get; set; }

    public int? UsuarioId { get; set; }

    public int? HabitacionId { get; set; }

    public int? Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual Habitacione? Habitacion { get; set; }

    public virtual Usuario? Usuario { get; set; }
}

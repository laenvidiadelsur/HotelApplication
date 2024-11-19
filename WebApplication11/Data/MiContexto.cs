using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Models;

namespace WebApplication11.Data;

public partial class MiContexto : DbContext
{
    public MiContexto()
    {
    }

    public MiContexto(DbContextOptions<MiContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TiposHabitacion> TiposHabitacions { get; set; }

    public virtual DbSet<UbicacionesHotel> UbicacionesHotels { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FEVPH2B\\SQLEXPRESS;Database=SistemaReservasHotel;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.HabitacionId).HasName("PK__Habitaci__FDB2639A85342C61");

            entity.Property(e => e.HabitacionId).HasColumnName("habitacion_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.NumeroHabitacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_habitacion");
            entity.Property(e => e.TipoId).HasColumnName("tipo_id");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.TipoId)
                .HasConstraintName("FK__Habitacio__tipo___5165187F");

            entity.HasMany(d => d.Servicios).WithMany(p => p.Habitacions)
                .UsingEntity<Dictionary<string, object>>(
                    "HabitacionesServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Habitacio__servi__628FA481"),
                    l => l.HasOne<Habitacione>().WithMany()
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Habitacio__habit__619B8048"),
                    j =>
                    {
                        j.HasKey("HabitacionId", "ServicioId").HasName("PK__Habitaci__2741C30A0390E338");
                        j.ToTable("Habitaciones_Servicios");
                        j.IndexerProperty<int>("HabitacionId").HasColumnName("habitacion_id");
                        j.IndexerProperty<int>("ServicioId").HasColumnName("servicio_id");
                    });
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__FFF0A58E19F96B18");

            entity.Property(e => e.PagoId).HasColumnName("pago_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK__Pagos__reserva_i__5CD6CB2B");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__Personal__C16BAC154B7E5A58");

            entity.ToTable("Personal");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Personal__5B8A0682E84C0358").IsUnique();

            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("puesto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__Reservas__F1437E4800321A13");

            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCheckIn).HasColumnName("fecha_check_in");
            entity.Property(e => e.FechaCheckOut).HasColumnName("fecha_check_out");
            entity.Property(e => e.HabitacionId).HasColumnName("habitacion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.HabitacionId)
                .HasConstraintName("FK__Reservas__habita__571DF1D5");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reservas__usuari__5629CD9C");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.ReseñaId).HasName("PK__Reseñas__33AE138A542BC305");

            entity.Property(e => e.ReseñaId).HasColumnName("reseña_id");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.HabitacionId).HasColumnName("habitacion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.HabitacionId)
                .HasConstraintName("FK__Reseñas__habitac__68487DD7");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reseñas__usuario__6754599E");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__AF3A090CB21B26BF");

            entity.Property(e => e.ServicioId).HasColumnName("servicio_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_servicio");
        });

        modelBuilder.Entity<TiposHabitacion>(entity =>
        {
            entity.HasKey(e => e.TipoId).HasName("PK__Tipos_Ha__6EA5A01B4A428218");

            entity.ToTable("Tipos_Habitacion");

            entity.Property(e => e.TipoId).HasColumnName("tipo_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo");
            entity.Property(e => e.PrecioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_base");
        });

        modelBuilder.Entity<UbicacionesHotel>(entity =>
        {
            entity.HasKey(e => e.UbicacionId).HasName("PK__Ubicacio__54512829DC64D294");

            entity.ToTable("Ubicaciones_Hotel");

            entity.Property(e => e.UbicacionId).HasColumnName("ubicacion_id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2ED7D2AF1A39B12E");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__5B8A06826B1EBBB0").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__D4D22D74469ACCB7").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class BdFacturacionContext : DbContext
    {
        public BdFacturacionContext()
        {
        }

        public BdFacturacionContext(DbContextOptions<BdFacturacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Existencia> Existencia { get; set; }
        public virtual DbSet<FacturaDet> FacturaDet { get; set; }
        public virtual DbSet<FacturaEnc> FacturaEnc { get; set; }
        public virtual DbSet<Iva> Iva { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=BdFacturacion;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.FechaCrea).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioActualiza)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCrea)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoIdentificacion)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.TipoIdentificacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_TipoIdentificacion");
            });

            modelBuilder.Entity<Existencia>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.UsuarioActualiza)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Existencia)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Existencia_Producto");
            });

            modelBuilder.Entity<FacturaDet>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorBruto).HasColumnType("money");

                entity.Property(e => e.ValorNeto).HasColumnType("money");

                entity.Property(e => e.ValorUnitario).HasColumnType("money");

                entity.HasOne(d => d.FacturaEnc)
                    .WithMany(p => p.FacturaDet)
                    .HasForeignKey(d => d.FacturaEncId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaDet_FacturaEnc");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.FacturaDet)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaDet_Producto");
            });

            modelBuilder.Entity<FacturaEnc>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.FechaCrea).HasColumnType("datetime");

                entity.Property(e => e.FechaFactura).HasColumnType("datetime");

                entity.Property(e => e.NumeroFactura).ValueGeneratedOnAdd();

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.TotalIva).HasColumnType("money");

                entity.Property(e => e.UsuarioActualiza)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCrea)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ValorDescuento).HasColumnType("money");

                entity.Property(e => e.ValorTotal).HasColumnType("money");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.FacturaEnc)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaEnc_Cliente");
            });

            modelBuilder.Entity<Iva>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.FechaCrea).HasColumnType("datetime");

                entity.Property(e => e.UsuarioActualiza)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCrea)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.FechaCrea).HasColumnType("datetime");

                entity.Property(e => e.Precio).HasColumnType("money");

                entity.Property(e => e.Unidad)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioActualiza)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCrea)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.Iva)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IvaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_Iva");
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualiza).HasColumnType("datetime");

                entity.Property(e => e.FechaCrea).HasColumnType("datetime");

                entity.Property(e => e.UsuarioActualiza)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCrea)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

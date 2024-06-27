using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemaGestionOrquesta.Models
{
    public partial class OrquestaOESATContext : DbContext
    {
        public OrquestaOESATContext()
        {
        }

        public OrquestaOESATContext(DbContextOptions<OrquestaOESATContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Curso> Cursos { get; set; } = null!;
        public virtual DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public virtual DbSet<Instrumento> Instrumentos { get; set; } = null!;
        public virtual DbSet<PrestamosInstrumento> PrestamosInstrumentos { get; set; } = null!;
        public virtual DbSet<Profesor> Profesors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=E000158\\SQLEXPRESS;Initial Catalog=OrquestaOESAT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("Curso");

                entity.HasIndex(e => e.ProfesorId, "IX_Curso_ProfesorID");

                entity.Property(e => e.CursoId).HasColumnName("CursoID");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.HasOne(d => d.Profesor)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.ProfesorId)
                    .HasConstraintName("FK__Curso__ProfesorI__4CA06362");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("Estudiante");

                entity.HasIndex(e => e.InstrumentoId, "IX_Estudiante_InstrumentoID");

                entity.Property(e => e.EstudianteId)
                    .HasColumnName("EstudianteID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Autoretiro).HasDefaultValueSql("((0))");

                entity.Property(e => e.Direccion).HasMaxLength(100);

                entity.Property(e => e.Documento).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EpPsicoMotriz)
                    .HasMaxLength(100)
                    .HasColumnName("Ep_psico_motriz")
                    .HasDefaultValueSql("('sin observaciones')");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.InstrumentoId).HasColumnName("InstrumentoID");

                entity.Property(e => e.Nacionalidad).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.NombreTutor)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Tutor");

                entity.Property(e => e.Particularidad)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('sin observaciones')");

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.Property(e => e.TelefonoTutor)
                    .HasMaxLength(20)
                    .HasColumnName("Telefono_Tutor");

                entity.Property(e => e.TmtMédico)
                    .HasMaxLength(100)
                    .HasColumnName("Tmt_médico")
                    .HasDefaultValueSql("('sin observaciones')");

                entity.HasOne(d => d.Instrumento)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.InstrumentoId)
                    .HasConstraintName("FK__Alumnos__Instrum__5629CD9C");

                entity.HasMany(d => d.Cursos)
                    .WithMany(p => p.Estudiantes)
                    .UsingEntity<Dictionary<string, object>>(
                        "AlumnosCurso",
                        l => l.HasOne<Curso>().WithMany().HasForeignKey("CursoId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__AlumnosCu__Curso__5535A963"),
                        r => r.HasOne<Estudiante>().WithMany().HasForeignKey("EstudianteId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__AlumnosCu__Alumn__5441852A"),
                        j =>
                        {
                            j.HasKey("EstudianteId", "CursoId").HasName("PK__AlumnosC__F7468990458DE6AB");

                            j.ToTable("AlumnosCurso");

                            j.HasIndex(new[] { "CursoId" }, "IX_AlumnosCurso_CursoID");

                            j.IndexerProperty<Guid>("EstudianteId").HasColumnName("EstudianteID");

                            j.IndexerProperty<int>("CursoId").HasColumnName("CursoID");
                        });
            });

            modelBuilder.Entity<Instrumento>(entity =>
            {
                entity.ToTable("Instrumento");

                entity.Property(e => e.InstrumentoId).HasColumnName("InstrumentoID");

                entity.Property(e => e.Detalles).HasMaxLength(200);

                entity.Property(e => e.Disponible)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<PrestamosInstrumento>(entity =>
            {
                entity.HasKey(e => e.PrestamoInstrumentoId);

                entity.HasIndex(e => e.EstudianteId, "IX_PrestamosInstrumentos_EstudianteId");

                entity.HasIndex(e => e.InstrumentoId, "IX_PrestamosInstrumentos_InstrumentoId");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.PrestamosInstrumentos)
                    .HasForeignKey(d => d.EstudianteId);

                entity.HasOne(d => d.Instrumento)
                    .WithMany(p => p.PrestamosInstrumentos)
                    .HasForeignKey(d => d.InstrumentoId);
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("Profesor");

                entity.Property(e => e.ProfesorId)
                    .HasColumnName("ProfesorID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(100);

                entity.Property(e => e.Documento).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

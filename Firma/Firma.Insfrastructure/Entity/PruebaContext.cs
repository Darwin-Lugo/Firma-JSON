using Firma.Core.Entitys;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace Firma.Insfrastructure.Entity;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    { }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    { }

    public virtual DbSet<Persona> Personas { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.Property(e => e.DateCrea).HasColumnType("date");
            entity.Property(e => e.Firma)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusData)
                .HasMaxLength(15)
                .IsUnicode(false);
        });
    }
}

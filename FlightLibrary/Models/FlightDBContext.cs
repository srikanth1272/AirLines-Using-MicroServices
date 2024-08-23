using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightLibrary.Models;

public partial class FlightDBContext : DbContext
{
    public FlightDBContext()
    {
    }

    public FlightDBContext(DbContextOptions<FlightDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=FlightDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNo).HasName("PK__Flight__8A9E3D4508D40096");

            entity.ToTable("Flight");

            entity.Property(e => e.FlightNo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FromCity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ToCity)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

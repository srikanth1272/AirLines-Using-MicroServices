using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleLibrary.Models;

public partial class FlightScheduleDBContext : DbContext
{
    public FlightScheduleDBContext()
    {
    }

    public FlightScheduleDBContext(DbContextOptions<FlightScheduleDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=FlightScheduleDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNo).HasName("PK__Flight__8A9E3D45022AE728");

            entity.ToTable("Flight");

            entity.Property(e => e.FlightNo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => new { e.FlightNo, e.FlightDate }).HasName("PK__FlightSc__BCFCC29DECA4DEE3");

            entity.ToTable("FlightSchedule");

            entity.Property(e => e.FlightNo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ArriveTime).HasColumnType("datetime");
            entity.Property(e => e.DepartTime).HasColumnType("datetime");

            entity.HasOne(d => d.FlightNoNavigation).WithMany(p => p.FlightSchedules)
                .HasForeignKey(d => d.FlightNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlightSch__Fligh__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

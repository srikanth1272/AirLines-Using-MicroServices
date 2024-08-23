using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReservationLibrary.Models;

public partial class ReservationDBContext : DbContext
{
    public ReservationDBContext()
    {
    }

    public ReservationDBContext(DbContextOptions<ReservationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<ReservationMaster> ReservationMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=ReservationDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => new { e.FlightNo, e.FlightDate }).HasName("PK__FlightSc__BCFCC29D5A46BCC9");

            entity.ToTable("FlightSchedule");

            entity.Property(e => e.FlightNo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<ReservationDetail>(entity =>
        {
            entity.HasKey(e => new { e.PNR, e.PassengerNo }).HasName("PK__Reservat__6DFE28AE6D720BFE");

            entity.ToTable("ReservationDetail");

            entity.Property(e => e.PNR)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PassengerName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.PNRNavigation).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.PNR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservation__PNR__3B75D760");
        });

        modelBuilder.Entity<ReservationMaster>(entity =>
        {
            entity.HasKey(e => e.PNR).HasName("PK__Reservat__C5773DD21A900CE5");

            entity.ToTable("ReservationMaster");

            entity.Property(e => e.PNR)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FlightNo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FlightSchedule).WithMany(p => p.ReservationMasters)
                .HasForeignKey(d => new { d.FlightNo, d.FlightDate })
                .HasConstraintName("FK__ReservationMaste__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

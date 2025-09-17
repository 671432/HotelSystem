using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelLibrary.Models;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:bordel69.database.windows.net,1433;Initial Catalog=Hotel;Persist Security Info=False;User ID=h671432;Password=1234567890aA;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("reservation");

            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.RoomNr).HasColumnName("room_nr");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("activ")
                .HasColumnName("status");
            entity.Property(e => e.ToDate).HasColumnName("to_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.RoomNrNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomNr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reservation_rom_nr");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_id");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNr).HasName("PK__room__1967F416DC2B20C2");

            entity.ToTable("room");

            entity.Property(e => e.RoomNr)
                .ValueGeneratedNever()
                .HasColumnName("room_nr");
            entity.Property(e => e.Beds).HasColumnName("beds");
            entity.Property(e => e.Quality)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("quality");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("available")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__tasks__0492148D5D04BE88");

            entity.ToTable("tasks");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.RoomNr).HasColumnName("room_nr");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.RoomNrNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.RoomNr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_task_room_nr");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_user");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

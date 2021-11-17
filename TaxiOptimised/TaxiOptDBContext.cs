using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaxiOptimised.Models;

#nullable disable

namespace TaxiOptimised
{
    public partial class TaxiOptDBContext : DbContext
    {
        public TaxiOptDBContext()
        {
        }

        public TaxiOptDBContext(DbContextOptions<TaxiOptDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverOrder> DriverOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-AB6K1OD;Initial Catalog=TaxiOptDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<DriverOrder>(entity =>
            {
                entity.HasKey(e => new { e.DriverId, e.OrderId });

                entity.ToTable("Driver_Order");

                /*entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DriverOrders)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Driver_Order_Drivers");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DriverOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Driver_Order_Orders");*/
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

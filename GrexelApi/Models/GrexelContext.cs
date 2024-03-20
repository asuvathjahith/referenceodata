using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrexelApi.Models;

public partial class GrexelContext : DbContext
{
    public GrexelContext()
    {
    }

    public GrexelContext(DbContextOptions<GrexelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ASPLAP3102;Database=Grexel;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressID).HasName("PK__Address__091C2A1B5E0E3BF4");

            entity.ToTable("Address");

            entity.Property(e => e.AddressID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.OrganizationID).HasColumnName("OrganizationID");
            entity.Property(e => e.Street).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.Organization).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.OrganizationID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Address__Organiz__300424B4");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationID).HasName("PK__Organiza__CADB0B7201A47DF9");

            entity.ToTable("Organization");

            entity.Property(e => e.OrganizationID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrganizationID");
            entity.Property(e => e.BusinessID)
                .HasMaxLength(50)
                .HasColumnName("BusinessID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

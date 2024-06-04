using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPF_Practice2024.Models;

public partial class DbforpraktikaContext : DbContext
{
    public DbforpraktikaContext()
    {
    }

    public DbforpraktikaContext(DbContextOptions<DbforpraktikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Demand> Demands { get; set; }

    public virtual DbSet<DemandApartment> DemandApartments { get; set; }

    public virtual DbSet<DemandHouse> DemandHouses { get; set; }

    public virtual DbSet<DemandLand> DemandLands { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Land> Lands { get; set; }

    public virtual DbSet<RealEstate> RealEstates { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-IM6F3Q9;Initial Catalog=dbforpraktika;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.IdAgent);

            entity.Property(e => e.IdAgent)
                .ValueGeneratedNever()
                .HasColumnName("Id_Agent");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.IdApartment);

            entity.Property(e => e.IdApartment).HasColumnName("Id_Apartment");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("Id_Client");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Demand>(entity =>
        {
            entity.HasKey(e => e.IdDemand);

            entity.Property(e => e.IdDemand).HasColumnName("Id_Demand");
            entity.Property(e => e.IdAgent).HasColumnName("Id_Agent");
            entity.Property(e => e.IdApartment).HasColumnName("Id_Apartment");
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.IdHouse).HasColumnName("Id_House");
            entity.Property(e => e.IdLand).HasColumnName("Id_Land");
            entity.Property(e => e.TypeRealEstate).HasColumnName("Type_RealEstate");

            entity.HasOne(d => d.IdAgentNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.IdAgent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Demands_Agents");

            entity.HasOne(d => d.IdApartmentNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.IdApartment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Demands_Demand_Apartments");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Demands_Clients");

            entity.HasOne(d => d.IdHouseNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.IdHouse)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Demands_Demand_Houses");

            entity.HasOne(d => d.IdLandNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.IdLand)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Demands_Demand_Lands");
        });

        modelBuilder.Entity<DemandApartment>(entity =>
        {
            entity.HasKey(e => e.IdApartment);

            entity.ToTable("Demand_Apartments");

            entity.Property(e => e.IdApartment).HasColumnName("Id_Apartment");
        });

        modelBuilder.Entity<DemandHouse>(entity =>
        {
            entity.HasKey(e => e.IdHouse);

            entity.ToTable("Demand_Houses");

            entity.Property(e => e.IdHouse).HasColumnName("Id_House");
        });

        modelBuilder.Entity<DemandLand>(entity =>
        {
            entity.HasKey(e => e.IdLand);

            entity.ToTable("Demand_Lands");

            entity.Property(e => e.IdLand).HasColumnName("Id_Land");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.IdDistrict);

            entity.Property(e => e.IdDistrict).HasColumnName("Id_District");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.IdHouse);

            entity.Property(e => e.IdHouse).HasColumnName("Id_House");
        });

        modelBuilder.Entity<Land>(entity =>
        {
            entity.HasKey(e => e.IdLand);

            entity.Property(e => e.IdLand).HasColumnName("Id_Land");
        });

        modelBuilder.Entity<RealEstate>(entity =>
        {
            entity.HasKey(e => e.IdRealEstate);

            entity.Property(e => e.IdRealEstate).HasColumnName("Id_RealEstate");
            entity.Property(e => e.AdressCity).HasColumnName("Adress_City");
            entity.Property(e => e.AdressHouse).HasColumnName("Adress_House");
            entity.Property(e => e.AdressNumber).HasColumnName("Adress_Number");
            entity.Property(e => e.AdressStreet).HasColumnName("Adress_Street");
            entity.Property(e => e.CoordinateLatitude).HasColumnName("Coordinate_latitude");
            entity.Property(e => e.CoordinateLongitude).HasColumnName("Coordinate_longitude");
            entity.Property(e => e.IdApartment).HasColumnName("Id_Apartment");
            entity.Property(e => e.IdDistrict).HasColumnName("Id_District");
            entity.Property(e => e.IdHouse).HasColumnName("Id_House");
            entity.Property(e => e.IdLand).HasColumnName("Id_Land");

            entity.HasOne(d => d.IdApartmentNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdApartment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RealEstates_Apartments");

            entity.HasOne(d => d.IdDistrictNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdDistrict)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RealEstates_Districts");

            entity.HasOne(d => d.IdHouseNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdHouse)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RealEstates_Houses");

            entity.HasOne(d => d.IdLandNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdLand)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RealEstates_Lands");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.IdSupply);

            entity.ToTable("Supply");

            entity.Property(e => e.IdSupply).HasColumnName("Id_Supply");
            entity.Property(e => e.IdAgent).HasColumnName("Id_Agent");
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.IdRealEstate).HasColumnName("Id_RealEstate");

            entity.HasOne(d => d.IdAgentNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdAgent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supply_Agents");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supply_Clients");

            entity.HasOne(d => d.IdRealEstateNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdRealEstate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supply_RealEstates");

            entity.HasMany(d => d.IdDemands).WithMany(p => p.IdSupplies)
                .UsingEntity<Dictionary<string, object>>(
                    "Deal",
                    r => r.HasOne<Demand>().WithMany()
                        .HasForeignKey("IdDemand")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Deals_Demands"),
                    l => l.HasOne<Supply>().WithMany()
                        .HasForeignKey("IdSupply")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Deals_Supply"),
                    j =>
                    {
                        j.HasKey("IdSupply", "IdDemand");
                        j.ToTable("Deals");
                        j.IndexerProperty<int>("IdSupply").HasColumnName("Id_Supply");
                        j.IndexerProperty<int>("IdDemand").HasColumnName("Id_Demand");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

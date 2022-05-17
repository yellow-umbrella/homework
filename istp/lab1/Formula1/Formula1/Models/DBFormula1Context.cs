using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Formula1
{
    public partial class DBFormula1Context : DbContext
    {
        public DBFormula1Context()
        {
        }

        public DBFormula1Context(DbContextOptions<DBFormula1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Circuite> Circuites { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<DriverActivity> DriverActivities { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<RaceResult> RaceResults { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TyreSupplier> TyreSuppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= LAPTOP-9IS59JQ1;Database=DBFormula1; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Circuite>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Circuites)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Circuites_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Drivers_Countries");
            });

            modelBuilder.Entity<DriverActivity>(entity =>
            {
                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DriverActivities)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DriverActivities_Drivers");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.DriverActivities)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DriverActivities_Seasons");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.DriverActivities)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DriverActivities_Teams");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Circuite)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.CircuiteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Races_Circuites");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Races_Seasons");
            });

            modelBuilder.Entity<RaceResult>(entity =>
            {
                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.RaceResults)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RaceResults_Drivers");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.RaceResults)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RaceResults_Races");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.Property(e => e.RaceDirector).HasMaxLength(50);

                //entity.Property(e => e.Rules).HasMaxLength(50);

                entity.HasOne(d => d.TyreSupplier)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(d => d.TyreSupplierId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Seasons_TyreSuppliers");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TyreSupplier>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

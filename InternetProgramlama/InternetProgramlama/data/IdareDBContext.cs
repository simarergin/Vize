using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InternetProgramlama.data
{
    public partial class IdareDBContext : DbContext
    {
        public IdareDBContext()
        {
        }

        public IdareDBContext(DbContextOptions<IdareDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DersTablo> DersTablos { get; set; } = null!;
        public virtual DbSet<OgrenciDersTablo> OgrenciDersTablos { get; set; } = null!;
        public virtual DbSet<OgrenciTablo> OgrenciTablos { get; set; } = null!;
        public virtual DbSet<OkulYonetimTablo> OkulYonetimTablos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=RIDDARC\\SQLEXPRESS;Database=IdareDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DersTablo>(entity =>
            {
                entity.ToTable("DersTablo");

                entity.Property(e => e.Ad).HasMaxLength(50);

                entity.HasOne(d => d.OkulYonetim)
                    .WithMany(p => p.DersTablos)
                    .HasForeignKey(d => d.OkulYonetimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DersTablo_OkulYonetimTablo1");
            });

            modelBuilder.Entity<OgrenciDersTablo>(entity =>
            {
                entity.ToTable("OgrenciDersTablo");

                entity.HasOne(d => d.Ders)
                    .WithMany(p => p.OgrenciDersTablos)
                    .HasForeignKey(d => d.DersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OgrenciDersTablo_DersTablo1");

                entity.HasOne(d => d.Ogrenci)
                    .WithMany(p => p.OgrenciDersTablos)
                    .HasForeignKey(d => d.OgrenciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OgrenciDersTablo_OgrenciTablo1");
            });

            modelBuilder.Entity<OgrenciTablo>(entity =>
            {
                entity.ToTable("OgrenciTablo");

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.Bolum).HasMaxLength(50);

                entity.Property(e => e.DTarih)
                    .HasColumnType("date")
                    .HasColumnName("DTarih");
            });

            modelBuilder.Entity<OkulYonetimTablo>(entity =>
            {
                entity.ToTable("OkulYonetimTablo");

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.Gorevi).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InternetProgramlama.data
{
    public partial class OkulIdareContext : DbContext
    {
        public OkulIdareContext()
        {
        }

        public OkulIdareContext(DbContextOptions<OkulIdareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Der> Ders { get; set; } = null!;
        public virtual DbSet<Ogrenci> Ogrencis { get; set; } = null!;
        public virtual DbSet<OgrenciDer> OgrenciDers { get; set; } = null!;
        public virtual DbSet<OkulYonetim> OkulYonetims { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=RIDDARC\\SQLEXPRESS;Database=OkulIdare;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Der>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ad).HasMaxLength(50);

                entity.HasOne(d => d.OkulYonetim)
                    .WithMany(p => p.Ders)
                    .HasForeignKey(d => d.OkulYonetimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ders_OkulYonetim");
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("Ogrenci");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.Bolum).HasMaxLength(50);

                entity.Property(e => e.Dtarih)
                    .HasColumnType("date")
                    .HasColumnName("DTarih");
            });

            modelBuilder.Entity<OgrenciDer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Ders)
                    .WithMany(p => p.OgrenciDers)
                    .HasForeignKey(d => d.DersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OgrenciDers_Ders");

                entity.HasOne(d => d.Ogrenci)
                    .WithMany(p => p.OgrenciDers)
                    .HasForeignKey(d => d.OgrenciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OgrenciDers_Ogrenci");
            });

            modelBuilder.Entity<OkulYonetim>(entity =>
            {
                entity.ToTable("OkulYonetim");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.Gorevi).HasMaxLength(50);

                entity.Property(e => e.YonetimTip).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

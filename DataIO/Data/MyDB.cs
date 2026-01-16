using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataIO.Data
{
    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }

        public virtual DbSet<BacDaoTao> BacDaoTaos { get; set; }
        public virtual DbSet<BC> BCs { get; set; }
        public virtual DbSet<CapChuyenMon> CapChuyenMons { get; set; }
        public virtual DbSet<CD> CDs { get; set; }
        public virtual DbSet<CD_TuongDuong> CD_TuongDuong { get; set; }
        public virtual DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public virtual DbSet<DV> DVs { get; set; }
        public virtual DbSet<DV_TuongDuong> DV_TuongDuong { get; set; }
        public virtual DbSet<HC> HCs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<QH> QHs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacDaoTao>()
                .HasMany(e => e.HCs)
                .WithRequired(e => e.BacDaoTao)
                .HasForeignKey(e => e.BacDaoTao_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BC>()
                .HasMany(e => e.HCs)
                .WithRequired(e => e.BC)
                .HasForeignKey(e => e.BC_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CapChuyenMon>()
                .HasMany(e => e.HCs)
                .WithRequired(e => e.CapChuyenMon)
                .HasForeignKey(e => e.CapChuyenMon_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CD>()
                .HasMany(e => e.BCs)
                .WithRequired(e => e.CD)
                .HasForeignKey(e => e.CD_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CD_TuongDuong>()
                .HasMany(e => e.CDs)
                .WithRequired(e => e.CD_TuongDuong)
                .HasForeignKey(e => e.CD_TD_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuyenNganh>()
                .HasMany(e => e.CDs)
                .WithRequired(e => e.ChuyenNganh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DV>()
                .HasMany(e => e.BCs)
                .WithRequired(e => e.DV)
                .HasForeignKey(e => e.DV_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DV>()
                .HasMany(e => e.DV1)
                .WithRequired(e => e.DV2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<DV_TuongDuong>()
                .HasMany(e => e.DVs)
                .WithRequired(e => e.DV_TuongDuong)
                .HasForeignKey(e => e.DV_TD_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nganh>()
                .HasMany(e => e.ChuyenNganhs)
                .WithRequired(e => e.Nganh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QH>()
                .Property(e => e.KieuCapHam)
                .IsFixedLength();

            modelBuilder.Entity<QH>()
                .HasMany(e => e.HCs)
                .WithRequired(e => e.QH)
                .HasForeignKey(e => e.QH_ID)
                .WillCascadeOnDelete(false);
        }
    }
}

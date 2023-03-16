using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PRN231PE_FA22_StudentCodeContext : DbContext
    {
        public PRN231PE_FA22_StudentCodeContext() { }
        public PRN231PE_FA22_StudentCodeContext(DbContextOptions<PRN231PE_FA22_StudentCodeContext> options)
        : base(options)
        {
        }
        public virtual DbSet<HRStaff> HRStaffs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PropertyProfile> PropertyProfiles { get; set; }
        public virtual DbSet<Renting> Renting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local); database=PRN231PE_FA22_StudentCode;uid=sa;pwd=NTQ@1234;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<HRStaff>(entity =>
            {
                entity.HasKey(e => e.EmailAddress);

                entity.ToTable("HRStaffs");
            });

            modelBuilder.Entity<PropertyProfile>(entity =>
            {
                entity.Property(e => e.PropertyProfileID).HasColumnName("PropertyProfileID");
            });

            modelBuilder.Entity<Renting>(entity =>
            {
                entity.HasKey(e => new { e.PropertyProfileID, e.CompanyID });

                entity.ToTable("Renting");

                entity.Property(e => e.PropertyProfileID).HasColumnName("PropertyProfileID");
                entity.Property(e => e.CompanyID).HasColumnName("CompanyID");

                entity.HasOne(d => d.Company).WithMany(p => p.Rentings)
                    .HasForeignKey(d => d.CompanyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Renting_Companies");

                entity.HasOne(d => d.PropertyProfile).WithMany(p => p.Rentings)
                    .HasForeignKey(d => d.PropertyProfileID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Renting_PropertyProfiles");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

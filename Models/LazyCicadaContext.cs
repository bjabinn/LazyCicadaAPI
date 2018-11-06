using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LazyCicadaApi.Models
{
    public partial class LazyCicadaContext : DbContext
    {
        public LazyCicadaContext()
        {
        }

        public LazyCicadaContext(DbContextOptions<LazyCicadaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleEmployee> RoleEmployee { get; set; }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=misslazycicada;Password=root;Database=lazycicada;");
            }
        } */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EpyPk);

                entity.ToTable("employee", "headcount");

                entity.Property(e => e.EpyPk)
                    .HasColumnName("epy_pk")
                    .HasDefaultValueSql("nextval('headcount.employee_epy_pk_seq'::regclass)");

                entity.Property(e => e.EpyFirstName)
                    .IsRequired()
                    .HasColumnName("epy_first_name")
                    .HasColumnType("character varying(50)");

                entity.Property(e => e.EpyLastName)
                    .IsRequired()
                    .HasColumnName("epy_last_name")
                    .HasColumnType("character varying(50)");

                entity.Property(e => e.EpyNumber).HasColumnName("epy_number");

                entity.Property(e => e.EpyShortName)
                    .IsRequired()
                    .HasColumnName("epy_short_name")
                    .HasColumnType("character varying(50)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolPk);

                entity.ToTable("role", "headcount");

                entity.Property(e => e.RolPk)
                    .HasColumnName("rol_pk")
                    .HasDefaultValueSql("nextval('headcount.role_rol_pk_seq'::regclass)");

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasColumnName("rol_name")
                    .HasColumnType("character varying(50)");
            });

            modelBuilder.Entity<RoleEmployee>(entity =>
            {
                entity.HasKey(e => e.RolEpyPk);

                entity.ToTable("role_employee", "headcount");

                entity.Property(e => e.RolEpyPk)
                    .HasColumnName("rol_epy_pk")
                    .HasDefaultValueSql("nextval('headcount.role_employee_rol_epy_pk_seq'::regclass)");

                entity.Property(e => e.EpyId).HasColumnName("epy_id");

                entity.Property(e => e.RolId).HasColumnName("rol_id");
            });

            modelBuilder.HasSequence("employee_epy_pk_seq");

            modelBuilder.HasSequence("role_employee_rol_epy_pk_seq");

            modelBuilder.HasSequence("role_rol_pk_seq");
        }
    }
}

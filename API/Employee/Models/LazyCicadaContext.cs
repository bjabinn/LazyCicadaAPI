using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LazyCicada.API.Models
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
                entity.HasKey(e => e.Pk);

                entity.ToTable("employee", "headcount");

                entity.HasIndex(e => e.EmployeeNumber)
                    .HasName("employee_employee_number_key")
                    .IsUnique();

                entity.HasIndex(e => e.SamAccountName)
                    .HasName("employee_sam_account_name_key")
                    .IsUnique();

                entity.Property(e => e.Pk)
                    .HasColumnName("pk")
                    .HasDefaultValueSql("nextval('headcount.employee_pk_seq'::regclass)");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnName("display_name")
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeNumber).HasColumnName("employee_number");

                entity.Property(e => e.SamAccountName)
                    .IsRequired()
                    .HasColumnName("sam_account_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Pk);

                entity.ToTable("role", "headcount");

                entity.HasIndex(e => e.Name)
                    .HasName("role_name_key")
                    .IsUnique();

                entity.Property(e => e.Pk)
                    .HasColumnName("pk")
                    .HasDefaultValueSql("nextval('headcount.role_pk_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoleEmployee>(entity =>
            {
                entity.HasKey(e => e.Pk);

                entity.ToTable("role_employee", "headcount");

                entity.HasIndex(e => new { e.Role, e.Employee })
                    .HasName("role_employee_role_employee_key")
                    .IsUnique();

                entity.Property(e => e.Pk)
                    .HasColumnName("pk")
                    .HasDefaultValueSql("nextval('headcount.role_employee_pk_seq'::regclass)");

                entity.Property(e => e.Employee).HasColumnName("employee");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.RoleEmployee)
                    .HasForeignKey(d => d.Employee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_employee_employee_fkey");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.RoleEmployee)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_employee_role_fkey");
            });

            modelBuilder.HasSequence("employee_pk_seq");

            modelBuilder.HasSequence("role_employee_pk_seq");

            modelBuilder.HasSequence("role_pk_seq");
        }
    }
}

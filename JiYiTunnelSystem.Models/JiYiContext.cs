namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class JiYiContext : DbContext
    {
        public JiYiContext()
            : base("name=JiYiContext")
        {
        }

        public virtual DbSet<engineeringsites> engineeringsites { get; set; }
        public virtual DbSet<logs> logs { get; set; }
        public virtual DbSet<offsets> offsets { get; set; }
        public virtual DbSet<sections> sections { get; set; }
        public virtual DbSet<sensors> sensors { get; set; }
        public virtual DbSet<steelstresses> steelstresses { get; set; }
        public virtual DbSet<strains> strains { get; set; }
        public virtual DbSet<stresses> stresses { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<vibrations> vibrations { get; set; }
        public virtual DbSet<alarmlogs> alarmlogs { get; set; }
        public virtual DbSet<messages> messages { get; set; }
        public virtual DbSet<setvaluelogs> setvaluelogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<engineeringsites>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<engineeringsites>()
                .HasMany(e => e.sections)
                .WithRequired(e => e.engineeringsites)
                .HasForeignKey(e => e.EngId);

            modelBuilder.Entity<logs>()
                .Property(e => e.Behavior)
                .IsUnicode(false);

            modelBuilder.Entity<sections>()
                .Property(e => e.Shaft)
                .IsUnicode(false);

            modelBuilder.Entity<sections>()
                .Property(e => e.SectionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.offsets)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.sensors)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.steelstresses)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.strains)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.stresses)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.vibrations)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.alarmlogs)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sections>()
                .HasMany(e => e.setvaluelogs)
                .WithRequired(e => e.sections)
                .HasForeignKey(e => e.SectionId);

            modelBuilder.Entity<sensors>()
                .Property(e => e.SensorType)
                .IsUnicode(false);

            modelBuilder.Entity<sensors>()
                .Property(e => e.SensorNumber)
                .IsUnicode(false);

            modelBuilder.Entity<sensors>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<sensors>()
                .HasMany(e => e.alarmlogs)
                .WithRequired(e => e.sensors)
                .HasForeignKey(e => e.SensorId);

            modelBuilder.Entity<sensors>()
                .HasMany(e => e.setvaluelogs)
                .WithRequired(e => e.sensors)
                .HasForeignKey(e => e.SensorId);

            modelBuilder.Entity<users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.logs)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<messages>()
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}

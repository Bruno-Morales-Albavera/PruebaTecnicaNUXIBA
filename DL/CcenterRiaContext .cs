using Microsoft.EntityFrameworkCore;
using ML;

namespace DL
{
    public partial class CcenterRiaContext : DbContext
    {
        public CcenterRiaContext() { }

        public CcenterRiaContext(DbContextOptions<CcenterRiaContext> options)
            : base(options) { }

        public virtual DbSet<ML.CcLogLogin> Ccloglogins { get; set; }
        public virtual DbSet<CcUser> CcUsers { get; set; }
        public virtual DbSet<CcRiaCatArea> CcAreas { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=CCenterRIA2;" +
            "TrustServerCertificate=True;User ID=sa;" +
            "Password=YourStrong!Passw0rd;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CcLogLogin>(entity =>
            {
                entity.ToTable("ccloglogin");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
            });

            modelBuilder.Entity<CcUser>(entity =>
            {
                entity.ToTable("ccUsers");
            });

            modelBuilder.Entity<CcRiaCatArea>(entity =>
            {
                entity.ToTable("ccRIACat_Areas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

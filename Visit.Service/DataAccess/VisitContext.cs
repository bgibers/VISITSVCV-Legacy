using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using visitsvc.Models;

namespace visitsvc.DataAccess
{
    public partial class VisitContext : IdentityDbContext<User>
    {
        public VisitContext()
        {
        }

        public VisitContext(DbContextOptions<VisitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLocation> UserLocation { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=visit-db.mysql.database.azure.com;port=3306;user=TeamVisit@visit-db;password=Clemson17;database=visit");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationId")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.Avi)
                    .HasColumnName("avi")
                    .HasColumnType("longtext");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");
                
                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("fName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FacebookId)
                    .HasColumnName("facebookId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("lName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.HasIndex(e => e.LocationId)
                    .HasName("LocationId");

                entity.HasIndex(e => e.UserId)
                    .HasName("userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasColumnName("LocationId")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SpecialCase)
                    .IsRequired()
                    .HasColumnName("specialCase")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ToVisit)
                    .HasColumnName("toVisit")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                
                entity.Property(e => e.Visited)
                    .HasColumnName("visited")
                    .HasColumnType("bit(1)");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.UserLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserLocation_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserLocation_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

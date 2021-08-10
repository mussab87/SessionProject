using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class TraineeDbContext : DbContext
    {
        public TraineeDbContext()
        {
        }

        public TraineeDbContext(DbContextOptions<TraineeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<CourseTbl> CourseTbls { get; set; }
        public virtual DbSet<TeacherCourseTbl> TeacherCourseTbls { get; set; }
        public virtual DbSet<TeacherTbl> TeacherTbls { get; set; }
        public virtual DbSet<TraineeCourseTable> TraineeCourseTables { get; set; }
        public virtual DbSet<TraineeTbl> TraineeTbls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MUSSAB\\SQLEXPRESS;Database=SessionDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<CourseTbl>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("Course_Tbl");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.CourseNameArabic)
                    .HasMaxLength(50)
                    .HasColumnName("Course_Name_Arabic");

                entity.Property(e => e.CourseNameEnglish)
                    .HasMaxLength(50)
                    .HasColumnName("Course_Name_English");
            });

            modelBuilder.Entity<TeacherCourseTbl>(entity =>
            {
                entity.HasKey(e => e.TeacherTrsId);

                entity.ToTable("Teacher_Course_Tbl");

                entity.Property(e => e.TeacherTrsId).HasColumnName("Teacher_Trs_Id");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourseTbls)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Course_Tbl_Course_Tbl");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherCourseTbls)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Course_Tbl_Teacher_Tbl");
            });

            modelBuilder.Entity<TeacherTbl>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.ToTable("Teacher_Tbl");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

                entity.Property(e => e.CreateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_STAMP");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(250)
                    .HasColumnName("Teacher_Name");

                entity.Property(e => e.TeacherNationalId)
                    .HasMaxLength(50)
                    .HasColumnName("Teacher_National_Id");

                entity.Property(e => e.TeacherPhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Teacher_Phone_Number");

                entity.Property(e => e.UpdateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_STAMP");
            });

            modelBuilder.Entity<TraineeCourseTable>(entity =>
            {
                entity.HasKey(e => e.TraineeTrsId);

                entity.ToTable("Trainee_Course_Table");

                entity.Property(e => e.TraineeTrsId).HasColumnName("Trainee_Trs_Id");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.TraineeId).HasColumnName("Trainee_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TraineeCourseTables)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainee_Course_Table_Course_Tbl");

                entity.HasOne(d => d.Trainee)
                    .WithMany(p => p.TraineeCourseTables)
                    .HasForeignKey(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainee_Course_Table_Trainee_Tbl");
            });

            modelBuilder.Entity<TraineeTbl>(entity =>
            {
                entity.HasKey(e => e.TraineeId);

                entity.ToTable("Trainee_Tbl");

                entity.Property(e => e.TraineeId).HasColumnName("Trainee_Id");

                entity.Property(e => e.CreateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_STAMP");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.TraineeName)
                    .HasMaxLength(250)
                    .HasColumnName("Trainee_Name");

                entity.Property(e => e.TraineeNationalId)
                    .HasMaxLength(50)
                    .HasColumnName("Trainee_National_Id");

                entity.Property(e => e.UpdateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_STAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

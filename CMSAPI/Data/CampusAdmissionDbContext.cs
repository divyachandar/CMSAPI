using System;
using System.Collections.Generic;
using CMSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMSAPI.Data;

public partial class CampusAdmissionDbContext : DbContext
{
    public CampusAdmissionDbContext()
    {
    }

    public CampusAdmissionDbContext(DbContextOptions<CampusAdmissionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmissionApplication> AdmissionApplications { get; set; }

    public virtual DbSet<AdmissionDocument> AdmissionDocuments { get; set; }

    public virtual DbSet<AdmissionStatusHistory> AdmissionStatusHistories { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookIssue> BookIssues { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<FeeStructure> FeeStructures { get; set; }

    public virtual DbSet<InstitutionSetting> InstitutionSettings { get; set; }

    public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SecuritySetting> SecuritySettings { get; set; }

    public virtual DbSet<StaffProfile> StaffProfiles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

    public virtual DbSet<StudentTransportAssignment> StudentTransportAssignments { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TransportRoute> TransportRoutes { get; set; }

    public virtual DbSet<TransportRouteStop> TransportRouteStops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<VwStudentDistribution> VwStudentDistributions { get; set; }

    public virtual DbSet<VwStudentTransport> VwStudentTransports { get; set; }

    public virtual DbSet<VwTransportRoute> VwTransportRoutes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmissionApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId);

            entity.HasIndex(e => e.CampusId, "IX_AdmissionApplications_CampusId");

            entity.HasIndex(e => e.CourseId, "IX_AdmissionApplications_CourseId");

            entity.HasIndex(e => e.Status, "IX_AdmissionApplications_Status");

            entity.HasIndex(e => e.ApplicationNumber, "UQ__Admissio__5F2CD2C407767C3F").IsUnique();

            entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplicationNumber).HasMaxLength(50);
            entity.Property(e => e.AppliedDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentName).HasMaxLength(100);

            entity.HasOne(d => d.Campus).WithMany(p => p.AdmissionApplications)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdmissionApplications_Campus");

            entity.HasOne(d => d.Course).WithMany(p => p.AdmissionApplications)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdmissionApplications_Course");
        });

        modelBuilder.Entity<AdmissionDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Admissio__1ABEEF0FCA5AC944");

            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Application).WithMany(p => p.AdmissionDocuments)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_AdmissionDocuments_Application");
        });

        modelBuilder.Entity<AdmissionStatusHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Admissio__4D7B4ABD752DA09E");

            entity.ToTable("AdmissionStatusHistory");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ChangedBy).HasMaxLength(100);
            entity.Property(e => e.NewStatus).HasMaxLength(20);
            entity.Property(e => e.OldStatus).HasMaxLength(20);

            entity.HasOne(d => d.Application).WithMany(p => p.AdmissionStatusHistories)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdmissionStatusHistory_Application");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.Isbn, "UQ_Books_ISBN").IsUnique();

            entity.Property(e => e.BookId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Author).HasMaxLength(150);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<BookIssue>(entity =>
        {
            entity.Property(e => e.BookIssueId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasMaxLength(50);

            entity.HasOne(d => d.Book).WithMany(p => p.BookIssues)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookIssues_Book");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.CampusId).HasName("PK__Campuses__FD598DD618AAF2F7");

            entity.Property(e => e.CampusName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A781441540");

            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED11705933");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC3456529DE0").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exams__297521C7C318ED3D");

            entity.Property(e => e.ExamName).HasMaxLength(150);

            entity.HasOne(d => d.Course).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exams_Course");
        });

        modelBuilder.Entity<FeeStructure>(entity =>
        {
            entity.HasKey(e => e.FeeStructureId).HasName("PK__FeeStruc__DDDC2504AE2AFE00");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.FeeStructures)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeeStructure_Course");
        });

        modelBuilder.Entity<InstitutionSetting>(entity =>
        {
            entity.HasKey(e => e.InstitutionId).HasName("PK__Institut__8DF6B6AD958AE015");

            entity.HasIndex(e => e.InstitutionCode, "UQ__Institut__4D4898491E201DFB").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.InstitutionCode).HasMaxLength(50);
            entity.Property(e => e.InstitutionName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Website).HasMaxLength(200);
        });

        modelBuilder.Entity<NotificationSetting>(entity =>
        {
            entity.HasKey(e => e.NotificationSettingId).HasName("PK__Notifica__42A1C9A2ADB47423");

            entity.Property(e => e.EnableEmailNotifications).HasDefaultValue(true);
            entity.Property(e => e.EnableNotifications).HasDefaultValue(true);
            entity.Property(e => e.EnableSmsnotifications).HasColumnName("EnableSMSNotifications");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AB274FC8D");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160029F3130").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SecuritySetting>(entity =>
        {
            entity.HasKey(e => e.SecuritySettingId).HasName("PK__Security__FC2E44026FB54587");

            entity.Property(e => e.RequireStrongPasswords).HasDefaultValue(true);
            entity.Property(e => e.SessionTimeoutMinutes).HasDefaultValue(30);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<StaffProfile>(entity =>
        {
            entity.HasKey(e => e.StaffProfileId).HasName("PK__StaffPro__0A2981B651FE72AC");

            entity.HasIndex(e => e.UserId, "UQ__StaffPro__1788CC4D4579ACA5").IsUnique();

            entity.Property(e => e.Designation).HasMaxLength(100);

            entity.HasOne(d => d.User).WithOne(p => p.StaffProfile)
                .HasForeignKey<StaffProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffProfiles_Users");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B992DB5ABDE");

            entity.HasIndex(e => e.StudentCode, "UQ__Students__1FC8860471B49C6B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Students__A9D10534450BA210").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Batch).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.GuardianEmail).HasMaxLength(150);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.GuardianPhone).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.StudentCode).HasMaxLength(30);

            entity.HasOne(d => d.Campus).WithMany(p => p.Students)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Campuses");

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Courses");
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentProfileId).HasName("PK__StudentP__222BD0B04D036A37");

            entity.HasIndex(e => e.UserId, "UQ__StudentP__1788CC4DDDE88461").IsUnique();

            entity.HasIndex(e => e.RollNumber, "UQ__StudentP__E9F06F16C34D95AA").IsUnique();

            entity.Property(e => e.RollNumber).HasMaxLength(50);

            entity.HasOne(d => d.User).WithOne(p => p.StudentProfile)
                .HasForeignKey<StudentProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProfiles_Users");
        });

        modelBuilder.Entity<StudentTransportAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__StudentT__32499E77CF0CD4E5");

            entity.HasIndex(e => e.Status, "IX_StudentTransport_Status");

            entity.HasIndex(e => e.StudentId, "IX_StudentTransport_Student");

            entity.HasIndex(e => new { e.StudentId, e.RouteId }, "UQ_Student_Route").IsUnique();

            entity.Property(e => e.AssignedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PickupPoint).HasMaxLength(150);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active");
            entity.Property(e => e.StudentId).HasMaxLength(30);

            entity.HasOne(d => d.Route).WithMany(p => p.StudentTransportAssignments)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentTransport_Route");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A86F56AD7E");

            entity.Property(e => e.SubjectName).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subjects_Course");
        });

        modelBuilder.Entity<TransportRoute>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Transpor__80979B4DBD2AB1AF");

            entity.HasIndex(e => e.Status, "IX_TransportRoutes_Status");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DriverName).HasMaxLength(150);
            entity.Property(e => e.DriverPhone).HasMaxLength(30);
            entity.Property(e => e.RouteName).HasMaxLength(150);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active");
            entity.Property(e => e.VehicleNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<TransportRouteStop>(entity =>
        {
            entity.HasKey(e => e.StopId).HasName("PK__Transpor__EB6A38F4C3E17F29");

            entity.HasIndex(e => new { e.RouteId, e.StopOrder }, "UQ_Route_StopOrder").IsUnique();

            entity.Property(e => e.StopName).HasMaxLength(150);

            entity.HasOne(d => d.Route).WithMany(p => p.TransportRouteStops)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK_RouteStops_Routes");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBAB2E4F8");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E79ABB51").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__UserProf__290C88E4FA2F6052");

            entity.HasIndex(e => e.UserId, "UQ__UserProf__1788CC4D7F0542B6").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_UserProfiles_Departments");

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfiles_Users");
        });

        modelBuilder.Entity<VwStudentDistribution>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_StudentDistribution");

            entity.Property(e => e.CourseName).HasMaxLength(100);
        });

        modelBuilder.Entity<VwStudentTransport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_StudentTransport");

            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PickupPoint).HasMaxLength(150);
            entity.Property(e => e.RouteName).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasMaxLength(30);
        });

        modelBuilder.Entity<VwTransportRoute>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TransportRoutes");

            entity.Property(e => e.DriverName).HasMaxLength(150);
            entity.Property(e => e.DriverPhone).HasMaxLength(30);
            entity.Property(e => e.RouteId).ValueGeneratedOnAdd();
            entity.Property(e => e.RouteName).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.VehicleNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

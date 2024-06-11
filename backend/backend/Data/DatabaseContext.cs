using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace backend.Data
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Student)
                .WithMany(p => p.Subjects)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Teacher>()
                .HasOne(s => s.Subject)
                .WithMany(p => p.Teachers)
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdentityRole>().HasData(
              new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            AppUser kovi = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "kovi91@gmail.com",
                EmailConfirmed = true,
                UserName = "kovi91@gmail.com",
                FirstName = "Kovács",
                LastName = "András",
                NormalizedUserName = "KOVI91@GMAIL.COM"
            };
            kovi.PasswordHash = ph.HashPassword(kovi, "almafa123");
            modelBuilder.Entity<AppUser>().HasData(kovi);

            Student student1 = new Student { Id = Guid.NewGuid().ToString(), Name = "John Doe", Neptun = "ABC123", Image = "https://xsgames.co/randomusers/assets/avatars/pixel/0.jpg" };
            Student student2 = new Student { Id = Guid.NewGuid().ToString(), Name = "Jane Smith", Neptun = "DEF456", Image = "https://xsgames.co/randomusers/assets/avatars/pixel/1.jpg" };
            // Add test data for Students
            modelBuilder.Entity<Student>().HasData(
                student1,student2
            );

            Subject subject1 = new Subject { Id = Guid.NewGuid().ToString(), Name = "Math", Neptun = "MATH101", Credit = 4, Exam = true, Image = "https://fastly.picsum.photos/id/229/200/300.jpg?hmac=WD1_MXzGKrVpaJj2Utxv7FoijRJ6h4S4zrBj7wmsx1U", StudentId = student1.Id};
            Subject subject2 = new Subject { Id = Guid.NewGuid().ToString(), Name = "Science", Neptun = "SCI202", Credit = 3, Exam = false, Image = "https://fastly.picsum.photos/id/604/200/300.jpg?hmac=6ceMKS8u7easDoKzWSaIiSTpRlTPn1OUOdfSJWou3uQ", StudentId = student2.Id};
            // Add test data for Subjects
            modelBuilder.Entity<Subject>().HasData(
                subject1,subject2
            );

            // Add test data for Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = Guid.NewGuid().ToString(), Name = "Professor X", Neptun = "PROF01", Image = "https://xsgames.co/randomusers/assets/avatars/pixel/2.jpg", SubjectId = subject1.Id},
                new Teacher { Id = Guid.NewGuid().ToString(), Name = "Dr. Watson", Neptun = "DRWAT02", Image = "https://xsgames.co/randomusers/assets/avatars/pixel/3.jpg", SubjectId = subject1.Id}
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

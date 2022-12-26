using System.Security.Cryptography;
using System.Text;
using dsknowledgetestsback.Constants;
using dsknowledgetestsback.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dsknowledgetestsback.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CompletedTest> CompletedTests { get; set; }
        public DbSet<FeedbackCategory> FeedbackCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Faq> Faq { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string adminEmail = "admin@mail.ru";
            const string adminLogin = "admin@";
            const string adminPassword = "admin@";

            var adminRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = RolesConst.Admin.ToString()
            };
            var managerRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = RolesConst.Manager.ToString()
            };
            var userRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = RolesConst.User.ToString()
            };

            var educationBasic = new Education
            {
                Id = Guid.NewGuid(),
                Name = EducationConst.Basic.ToString()
            };
            var educationSecondary = new Education
            {
                Id = Guid.NewGuid(),
                Name = EducationConst.Secondary.ToString()
            };
            var educationSecondarySpecial = new Education
            {
                Id = Guid.NewGuid(),
                Name = EducationConst.SecondarySpecial.ToString()
            };
            var educationHigher = new Education
            {
                Id = Guid.NewGuid(),
                Name = EducationConst.Higher.ToString()
            };

            var questionTypeOneAnswer = new QuestionType
            {
                Id = Guid.NewGuid(),
                Name = QuestionTypeConst.OneAnswer.ToString()
            };
            var questionTypeMultipleAnswer = new QuestionType
            {
                Id = Guid.NewGuid(),
                Name = QuestionTypeConst.MultipleAnswer.ToString()
            };
            var questionTypeEnterAnswer = new QuestionType
            {
                Id = Guid.NewGuid(),
                Name = QuestionTypeConst.EnterAnswer.ToString()
            };

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Email = adminEmail,
                Login = adminLogin,
                Password = HaspPassword(adminPassword),
                DataCreated = DateTime.Now,
                IsActivated = true,
                IsDeleted = false,
                RoleId = adminRole.Id,
                EducationId = educationHigher.Id
            };

            var adminUserProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                FirstName = "",
                SurName = "",
                LastName = "",
                Birthday = new DateOnly(),
                Adress = "",
                PhoneNumber = "",
                UserId = adminUser.Id,
            };

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                adminRole,
                managerRole,
                userRole
            });
            modelBuilder.Entity<Education>().HasData(new Education[]
            {
                educationBasic,
                educationSecondary,
                educationSecondarySpecial,
                educationHigher
            });
            modelBuilder.Entity<QuestionType>().HasData(new QuestionType[]
            {
                questionTypeOneAnswer,
                questionTypeMultipleAnswer,
                questionTypeEnterAnswer,
            });
            modelBuilder.Entity<CompletedTest>()
                .HasOne(ct => ct.User)
                .WithOne(u => u.CompletedTest)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile[] { adminUserProfile });
            base.OnModelCreating(modelBuilder);
        }

        private static string HaspPassword(string password)
        {
            var md5 = MD5.Create();
            var b = Encoding.ASCII.GetBytes(password);
            var hash = md5.ComputeHash(b);
            var sb = new StringBuilder();
            foreach (var a in hash)
                sb.Append(a.ToString("X2"));
            return sb.ToString();
        }
    }

    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        {
        }
    }
}
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using dsknowledgetestsback.Constants;
using dsknowledgetestsback.Data;
using dsknowledgetestsback.Models;
using dsknowledgetestsback.ViewModels.UserViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface IUserService
    {
        public Task<UserViewModel?> GetByEmailAsync(string email);
        public Task<CreateUserViewModel?> CreateAsync(CreateUserViewModel user);
        public Task<EditUserViewModel?> EditAsync(EditUserViewModel user);
        public Task<EditUserViewModel?> EditStatusAsync(Guid id, bool status);
        public Task<List<ShowUserViewModel>> GetActivatedUsers();
        public Task<List<ShowUserViewModel>> GetUnactivatedUsers();
    }


    public class UserService : IUserService
    {
        public const int LOGIN_MIN_LENGHT = 6;
        public const int PASSWORD_MIN_LENGHT = 6;
        public const int EMAIL_MIN_LENGHT = 6;

        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserViewModel?> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _db.Users.AsNoTracking()
                    .Include("UserProfile")
                    .Include("Role")
                    .Include("Education")
                    .Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Login = u.Login,
                        Password = u.Password,
                        DataCreated = u.DataCreated.ToString(CultureInfo.InvariantCulture),
                        IsActivated = u.IsActivated,
                        IsDeleted = u.IsDeleted,
                        FirstName = u.UserProfile.FirstName,
                        SurName = u.UserProfile.SurName,
                        LastName = u.UserProfile.LastName,
                        Birthday = u.UserProfile.Birthday.ToString(),
                        Adress = u.UserProfile.Adress,
                        PhoneNumber = u.UserProfile.PhoneNumber,
                        RoleName = u.Role.Name,
                        EducationName = u.Education.Name,
                    }).FirstOrDefaultAsync(u =>
                        u.Email == email);
                return user?.IsActivated == true
                    ? user
                    : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CreateUserViewModel?> CreateAsync(CreateUserViewModel user)
        {
            try
            {
                if (user.Login.Length < LOGIN_MIN_LENGHT ||
                    user.Password.Length < PASSWORD_MIN_LENGHT ||
                    user.Email.Length < EMAIL_MIN_LENGHT) return null;

                await _db.Users.AddAsync(new User
                {
                    Email = user.Email,
                    Login = user.Login,
                    Password = HaspPassword(user.Password),
                    DataCreated = DateTime.Now,
                    RoleId = user.RoleId,
                    IsActivated = user.IsActivated,
                    IsDeleted = false,
                    EducationId = user.EducationId,
                });
                await _db.SaveChangesAsync();
                await _db.UserProfiles.AddAsync(new UserProfile
                {
                    FirstName = user.FirstName,
                    SurName = user.SurName,
                    LastName = user.LastName,
                    Birthday = DateOnly.Parse(user.Birthday),
                    Adress = user.Adress,
                    PhoneNumber = user.PhoneNumber,
                    UserId = (await _db.Users.AsNoTracking().FirstAsync(u => u.Email == user.Email)).Id
                });
                await _db.SaveChangesAsync();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<EditUserViewModel?> EditAsync(EditUserViewModel user)
        {
            try
            {
                if (user.Login.Length < LOGIN_MIN_LENGHT ||
                    user.Password.Length < PASSWORD_MIN_LENGHT ||
                    user.Email.Length < EMAIL_MIN_LENGHT) return null;

                var updateUser = await _db.Users.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (updateUser == null) return null;

                updateUser.Email = user.Email;
                updateUser.Login = user.Login;
                updateUser.Password = HaspPassword(user.Password);
                updateUser.RoleId = (await _db.Roles.AsNoTracking()
                    .FirstAsync(r => r.Name == user.RoleName)).Id;
                updateUser.EducationId = (await _db.Educations.AsNoTracking()
                    .FirstAsync(a => a.Name == user.EducationName)).Id;
                updateUser.IsActivated = user.IsActivated;
                updateUser.IsDeleted = user.IsDeleted;
                _db.Users.Update(updateUser);
                await _db.SaveChangesAsync();

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<EditUserViewModel?> EditStatusAsync(Guid id, bool status)
        {
            try
            {
                var updateUser = await _db.Users.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (updateUser == null) return null;

                updateUser.IsActivated = status;
                
                _db.Users.Update(updateUser);
                await _db.SaveChangesAsync();

                return new EditUserViewModel
                {
                    Id = updateUser.Id,
                    Email = updateUser.Email,
                    Login = updateUser.Login,
                    Password = updateUser.Password,
                    IsActivated = updateUser.IsActivated,
                    IsDeleted = updateUser.IsDeleted,
                    RoleName = (await _db.Roles.AsNoTracking().
                        FirstAsync(r => r.Id == updateUser.RoleId)).Name,
                    EducationName = (await _db.Educations.AsNoTracking().
                        FirstAsync(u => u.Id == updateUser.EducationId)).Name
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ShowUserViewModel>> GetActivatedUsers()
        {
            return await _db.Users.AsNoTracking()
                .Include("UserProfile")
                .Include("Role")
                .Include("Education")
                .Select(u => new ShowUserViewModel
                {
                    FirstName = u.UserProfile.FirstName,
                    SurName = u.UserProfile.SurName,
                    LastName = u.UserProfile.LastName,
                    Birthday = u.UserProfile.Birthday.ToString(),
                    Email = u.Email,
                    Login = u.Login,
                    Password = u.Password,
                    DataCreated = u.DataCreated.ToString(CultureInfo.CurrentCulture),
                    Adress = u.UserProfile.Adress,
                    RoleName = u.Role.Name,
                    EducationName = u.Education.Name,
                    PhoneNumber = u.UserProfile.PhoneNumber,
                    IsActivated = u.IsActivated,
                    IsDeleted = u.IsDeleted,
                }).Where(u => u.IsActivated)
                .ToListAsync();
        }

        public async Task<List<ShowUserViewModel>> GetUnactivatedUsers()
        {
            return await _db.Users.AsNoTracking()
                .Include("UserProfile")
                .Include("Role")
                .Include("Education")
                .Select(u => new ShowUserViewModel
                {
                    FirstName = u.UserProfile.FirstName,
                    SurName = u.UserProfile.SurName,
                    LastName = u.UserProfile.LastName,
                    Birthday = u.UserProfile.Birthday.ToString(),
                    Email = u.Email,
                    Login = u.Login,
                    Password = u.Password,
                    DataCreated = u.DataCreated.ToString(CultureInfo.CurrentCulture),
                    Adress = u.UserProfile.Adress,
                    RoleName = u.Role.Name,
                    EducationName = u.Education.Name,
                    PhoneNumber = u.UserProfile.PhoneNumber,
                    IsActivated = u.IsActivated,
                    IsDeleted = u.IsDeleted,
                }).Where(u => !u.IsActivated)
                .ToListAsync();
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
}
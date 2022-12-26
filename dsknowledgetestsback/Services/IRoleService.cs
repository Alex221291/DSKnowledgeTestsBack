using dsknowledgetestsback.Data;
using dsknowledgetestsback.Models;
using dsknowledgetestsback.ViewModels.RoleViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetAllAsync();
        Task<RoleViewModel?> CreateAsync(RoleViewModel role);
        Task<RoleViewModel?>? EditAsync(RoleViewModel role);
        Task<RoleViewModel?> DeleteAsync(Guid id);
        Task<RoleViewModel?> GetByNameAsync(string roleName);
        Task<string> GetNameByIdAsync(Guid? id);
    }

    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;

        public RoleService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllAsync()
        {
            return await _db.Roles.AsNoTracking().Select(r 
                => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<RoleViewModel?> CreateAsync(RoleViewModel role)
        {
            try
            {
                await _db.Roles.AddAsync(new Role
                {
                    Id = role.Id,
                    Name = role.Name
                });
                await _db.SaveChangesAsync();

                return role;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RoleViewModel?> EditAsync(RoleViewModel role)
        {
            try
            {
                var editRole = await _db.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == role.Id);

                if (editRole == null) return null;

                editRole.Name = role.Name;
                await _db.SaveChangesAsync();

                return role;
            }
            catch
            {
                return null;
            }
        }

        public async Task<RoleViewModel?> DeleteAsync(Guid id)
        {
            try
            {
                var deleteRole = await _db.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

                if (deleteRole == null) return null;

                _db.Roles.Remove(deleteRole);
                await _db.SaveChangesAsync();

                return new RoleViewModel
                {
                    Id = deleteRole.Id,
                    Name = deleteRole.Name
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<RoleViewModel?> GetByNameAsync(string roleName)
        {
            return await _db.Roles.AsNoTracking().Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).FirstOrDefaultAsync(r => r.Name == roleName);
        }
        public async Task<string> GetNameByIdAsync(Guid? id)
        {
            var role =  await _db.Roles.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            return role!.Name;
        }
    }
}
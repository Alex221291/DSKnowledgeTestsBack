using dsknowledgetestsback.Data;
using dsknowledgetestsback.Models;
using dsknowledgetestsback.ViewModels.AccountViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface IEducationService
    {
        Task<IEnumerable<EducationViewModel>> GetAllAsync();
        Task<EducationViewModel?> CreateAsync(EducationViewModel education);
        Task<EducationViewModel?>? EditAsync(EducationViewModel education);
        Task<EducationViewModel?> DeleteAsync(Guid id);
        Task<EducationViewModel?> GetByNameAsync(string educationName);
    }

    public class EducationService : IEducationService
    {
        private readonly AppDbContext _db;

        public EducationService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EducationViewModel>> GetAllAsync()
        {
            return await _db.Educations.AsNoTracking().Select(a => new EducationViewModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToListAsync();
        }

        public async Task<EducationViewModel?> CreateAsync(EducationViewModel education)
        {
            try
            {
                await _db.Educations.AddAsync(new Education
                {
                    Id = education.Id,
                    Name = education.Name
                });
                await _db.SaveChangesAsync();

                return education;
            }
            catch
            {
                return null;
            }
        }

        public async Task<EducationViewModel?>? EditAsync(EducationViewModel education)
        {
            try
            {
                var editAccountStatus = await _db.Educations.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == education.Id);

                if (editAccountStatus == null) return null;

                editAccountStatus.Name = education.Name;
                await _db.SaveChangesAsync();

                return education;
            }
            catch
            {
                return null;
            }
        }

        public async Task<EducationViewModel?> DeleteAsync(Guid id)
        {
            try
            {
                var deleteEducation = await _db.Educations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (deleteEducation == null) return null;

                _db.Educations.Remove(new Education { Id = id });
                await _db.SaveChangesAsync();

                return new EducationViewModel
                {
                    Id = deleteEducation.Id,
                    Name = deleteEducation.Name
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<EducationViewModel?> GetByNameAsync(string educationName)
        {
            return await _db.Educations.AsNoTracking().Select(a => new EducationViewModel
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefaultAsync(a => a.Name == educationName);
        }
    }
}
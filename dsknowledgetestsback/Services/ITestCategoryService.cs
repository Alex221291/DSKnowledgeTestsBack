using dsknowledgetestsback.Data;
using dsknowledgetestsback.ViewModels.TestCategoryViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface ITestCategoryService
    {
        public Task<List<TestCategoryViewModel>> GetAllAsync();
    }

    public class TestCategoryService : ITestCategoryService
    {
        private readonly AppDbContext _db;

        public TestCategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestCategoryViewModel>> GetAllAsync()
        {
            return await _db.TestCategories.AsNoTracking()
                .Include("Tests")
                .Select(t =>
                new TestCategoryViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Discription = t.Discription,
                    CountTests = t.Tests.Count
                }).ToListAsync();
        }
    }
}

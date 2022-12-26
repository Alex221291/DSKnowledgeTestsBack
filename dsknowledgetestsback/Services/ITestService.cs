using System.Globalization;
using dsknowledgetestsback.Data;
using dsknowledgetestsback.ViewModels.TestViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface ITestService
    {
        public Task<List<TestViewModel>> GetTestsForCategoryAsync(string categoryName);
    }

    public class TestService : ITestService
    {
        private readonly AppDbContext _db;

        public TestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestViewModel>> GetTestsForCategoryAsync(string categoryName)
        {
            return await _db.Tests.AsNoTracking()
                .Include("TestCategory")
                .Include("Questions")
                .Select(t => new TestViewModel
                {
                    Id = t.Id,
                    Subject = t.Subject,
                    Discription = t.Discription,
                    DataCreated = t.DataCreated.ToString(CultureInfo.InvariantCulture),
                    IsDeleted = t.IsDeleted,
                    CountQuestion = t.CountQuestion,
                    TestCategoryName = t.TestCategory.Name,
                    UserCreatedId = t.UserCreatedId,
                    Questions = t.Questions,
                })
                .Where(t => !t.IsDeleted && t.TestCategoryName == categoryName).ToListAsync();
        }
    }
}

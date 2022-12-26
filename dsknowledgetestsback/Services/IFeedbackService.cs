using System.Globalization;
using dsknowledgetestsback.Constants;
using dsknowledgetestsback.Data;
using dsknowledgetestsback.Models;
using dsknowledgetestsback.ViewModels.FeedbackViewModel;
using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Services
{
    public interface IFeedbackService
    {
        public Task<CreateFeedbackViewModel?> CreateAsync(CreateFeedbackViewModel model);
        
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _db;

        public FeedbackService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CreateFeedbackViewModel?> CreateAsync(CreateFeedbackViewModel model)
        {
            try
            {
                await _db.Feedbacks.AddAsync(new Feedback
                {
                    FirstName = model.FirstName,
                    SurName = model.SurName,
                    Email = model.Email,
                    Description = model.Description,
                    CreateData = DateTime.Now,
                    UserId = model.UserId,
                    FeedbackCategoryId = (await _db.FeedbackCategories.AsNoTracking()
                        .FirstAsync(fc => fc.Name == model.FeedbackCategoryName)).Id
                });
                await _db.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
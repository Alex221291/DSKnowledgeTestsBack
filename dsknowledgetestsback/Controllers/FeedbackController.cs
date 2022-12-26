using dsknowledgetestsback.Constants;
using dsknowledgetestsback.Services;
using dsknowledgetestsback.ViewModels.FeedbackViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

namespace dsknowledgetestsback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ObjectResult> Create(CreateFeedbackViewModel model)
        {
            var createFeedback = await _feedbackService.CreateAsync(model);
            if(createFeedback == null) return BadRequest(createFeedback);
            return Ok(createFeedback);
        }
    }
}

using dsknowledgetestsback.Services;
using dsknowledgetestsback.ViewModels.TestViewModel;
using Microsoft.AspNetCore.Mvc;

namespace dsknowledgetestsback.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [Route("GetTestsForCategory")]
        [HttpGet]
        public async Task<List<TestViewModel>> GetTestsForCategory(string categoryName) => 
            await _testService.GetTestsForCategoryAsync(categoryName);
        
    }
}

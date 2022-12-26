using dsknowledgetestsback.Services;
using dsknowledgetestsback.ViewModels.TestCategoryViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dsknowledgetestsback.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestCategoryController : ControllerBase
    {
        private readonly ITestCategoryService _testCategoryService;

        public TestCategoryController(ITestCategoryService testCategoryService)
        {
            _testCategoryService = testCategoryService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<TestCategoryViewModel>> GetAll() =>
            await _testCategoryService.GetAllAsync();
    }
}

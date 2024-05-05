using Microsoft.AspNetCore.Mvc;
using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Services.StoryService;

namespace Token_based_authentication_and_middleware.Controllers
{
    [Route("api/story")]
    [ApiController]
    public class StoryController : Controller
    {
        private readonly IStoryService storyService;

        public StoryController(IStoryService storyService)
        {
            this.storyService = storyService;
        }

        [HttpPost("save")]
        public BaseResponse CreateStory(CreateStoryRequest request)
        {
            return storyService.CreateStory(request);
        }

        [HttpGet("list")]
        public BaseResponse StoryList()
        {
            return storyService.StoryList();
        }
    }
}

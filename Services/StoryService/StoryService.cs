using Token_based_authentication_and_middleware.DTOs;
using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Models;

namespace Token_based_authentication_and_middleware.Services.StoryService
{
    public class StoryService : IStoryService 
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public StoryService(ApplicationDbContext context)
        {
            //get db context through DI
            this.context = context;
        }

        public BaseResponse CreateStory(CreateStoryRequest request)
        {
            BaseResponse response;

            try
            {
                //create new instance of story model
                StoryModel newStory = new StoryModel();
                newStory.title = request.title;
                newStory.description = request.description;
                newStory.user_id = request.user_id;

                using (context)
                {
                    context.Add(newStory);
                    context.SaveChanges();
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the story" }
                };

                return response;
            }catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse StoryList()
        {
            BaseResponse response;

            try
            {
                List<StoryDTO> stories = new List<StoryDTO>();

                using (context)
                {
                    //get all stories from the db and add to the story list after convert them to story dto
                    context.Stories.ToList().ForEach(story => stories.Add(new StoryDTO
                    {
                        Id = story.id,
                        Title = story.title,
                        Description = story.description,
                        UserID = story.user_id
                    }));
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { stories }
                };

                return response;
            }catch(Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error " + ex.Message }
                };

                return response;
            }
        }
    }
}

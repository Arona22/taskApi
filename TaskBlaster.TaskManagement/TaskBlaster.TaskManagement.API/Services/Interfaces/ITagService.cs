using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface ITagService
{
    /// <summary>
    /// Retrieves all tags asynchronously
    /// </summary>
    /// <returns>A collection of all tags</returns>
    Task<IEnumerable<TagDto>> GetAllTagsAsync();

    /// <summary>
    /// Creates a new tag asynchronously
    /// </summary>
    /// <param name="inputModel">The input model used to create the new tag</param>
    /// <returns>The ID of the newly created tag</returns>
    Task CreateNewTagAsync(TagInputModel inputModel);
}
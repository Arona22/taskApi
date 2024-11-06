using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface ITagRepository
{
    Task<IEnumerable<TagDto>> GetAllTagsAsync();
    Task CreateNewTagAsync(TagInputModel inputModel);
}
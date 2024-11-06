using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Returns a list of all registered users
    /// </summary>
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    
    /// <summary>
    /// Creates a user if it does not exist.
    /// </summary>
    /// <param name="inputModel">The input model used to create the user</param>
    Task CreateUserIfNotExistsAsync(UserInputModel inputModel);

    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="userId">The id of the user</param>
    Task<UserDto?> GetUserByIdAsync(int userId);
}
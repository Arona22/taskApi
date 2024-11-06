using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task CreateUserIfNotExists(UserInputModel inputModel);
    Task<UserDto?> GetUserByIdAsync(int userId);
}
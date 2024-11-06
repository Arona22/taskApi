using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskBlasterDbContext _context;

        public UserRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserIfNotExists(UserInputModel inputModel)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == inputModel.EmailAddress);

            if (existingUser == null)
            {
                var user = new Entities.User
                {
                    FullName = inputModel.FullName,
                    EmailAddress = inputModel.EmailAddress,
                    ProfileImageUrl = inputModel.ProfileImageUrl
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    EmailAddress = u.EmailAddress,
                    ProfileImageUrl = u.ProfileImageUrl
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    EmailAddress = u.EmailAddress,
                    ProfileImageUrl = u.ProfileImageUrl
                })
                .FirstOrDefaultAsync();
        }
    }
}

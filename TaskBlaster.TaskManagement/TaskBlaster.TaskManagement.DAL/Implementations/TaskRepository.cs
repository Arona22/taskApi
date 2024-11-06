using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskBlasterDbContext _context;

        public TaskRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task ArchiveTaskByIdAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                // Assuming 3 represents an archived status
                task.StatusId = 3;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignUserToTaskAsync(int taskId, int userId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.AssignedToId = userId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CreateNewTaskAsync(TaskInputModel task)
        {
            var newTask = new Entities.Task
            {
                Title = task.Title,
                Description = task.Description,
                StatusId = task.StatusId,
                PriorityId = task.PriorityId,
                DueDate = task.DueDate,
                CreatedAt = DateTime.Now
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return newTask.Id;
        }

        public async Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
        {
            var tasksQuery = _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .AsQueryable();

            // Filtering based on the provided criteria
            if (!string.IsNullOrEmpty(query.SearchValue))
                tasksQuery = tasksQuery.Where(t => t.Title.Contains(query.SearchValue));

            if (query.PriorityId.HasValue)
                tasksQuery = tasksQuery.Where(t => t.PriorityId == query.PriorityId);

            if (query.StatusId.HasValue)
                tasksQuery = tasksQuery.Where(t => t.StatusId == query.StatusId);

            // Count total items for pagination metadata
            var totalItems = await tasksQuery.CountAsync();

            // Apply pagination and project results to TaskDto
            var tasks = await tasksQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status.Name,
                    DueDate = t.DueDate,
                    AssignedToUser = t.AssignedTo != null ? t.AssignedTo.FullName : null
                })
                .ToListAsync();

            return new Envelope<TaskDto>
            {
                Items = tasks,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                MaxCount = totalItems
            };
        }




        public async Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null) return null;

            return new TaskDetailsDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.Name,
                Priority = task.Priority.Name,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedToUser = task.AssignedTo != null ? task.AssignedTo.FullName : null,
                Comments = task.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Author = c.User.FullName,
                    ContentAsMarkdown = c.ContentAsMarkdown,
                    CreatedDate = c.CreatedDate
                }).ToList(),
                Tags = task.Tags.Select(tag => tag.Name).ToList()
            };
        }

        public async Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
        {
            return await _context.Tasks
                .Where(t => t.DueDate <= DateTime.Now && 
                            !t.TaskNotifications.Any(n => n.DueDateNotificationSent))
                .Select(t => new TaskWithNotificationDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DueDate = t.DueDate,
                    AssignedToUser = t.AssignedTo != null ? t.AssignedTo.FullName : null // Include AssignedTo and get FullName
                })
                .ToListAsync();
        }



        public async Task UnassignUserFromTaskAsync(int taskId, int userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.AssignedToId == userId);
            if (task != null)
            {
                task.AssignedToId = null; // Unassign user
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTaskNotifications()
        {
            var tasks = await _context.Tasks
                .Where(t => t.DueDate <= DateTime.Now && 
                            !t.TaskNotifications.Any(n => n.DueDateNotificationSent))
                .ToListAsync();

            foreach (var task in tasks)
            {
                var notification = new Entities.TaskNotification
                {
                    TaskId = task.Id,
                    DueDateNotificationSent = true,  
                    LastNotificationDate = DateTime.Now
                };
                
                _context.TaskNotifications.Add(notification);
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.PriorityId = inputModel.PriorityId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.StatusId = inputModel.StatusId;
                await _context.SaveChangesAsync();
            }
        }
    }
}

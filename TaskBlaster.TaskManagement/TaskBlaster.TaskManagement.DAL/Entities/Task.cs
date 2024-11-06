using System;
using System.Collections.Generic;

namespace TaskBlaster.TaskManagement.DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int CreatedById { get; set; } // Foreign key to User who created the task
        public int? AssignedToId { get; set; } // Foreign key to User assigned to the task

        // Navigation Properties
        public User CreatedBy { get; set; } = null!;
        public User? AssignedTo { get; set; }
        public Status Status { get; set; } = null!;
        public Priority Priority { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<TaskNotification> TaskNotifications { get; set; } = new List<TaskNotification>();

    }
}

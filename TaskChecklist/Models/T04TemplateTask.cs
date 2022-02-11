using System;
using System.Collections.Generic;

namespace TaskChecklist.Models
{
    public partial class T04TemplateTask
    {
        public T04TemplateTask()
        {
            T02JobTasks = new HashSet<T02JobTask>();
        }

        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? TaskDesc { get; set; }
        public int TaskOrder { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? Active { get; set; }

        public virtual T03Template Template { get; set; } = null!;
        public virtual ICollection<T02JobTask> T02JobTasks { get; set; }
    }
}

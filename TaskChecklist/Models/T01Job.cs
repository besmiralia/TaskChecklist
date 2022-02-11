using System;
using System.Collections.Generic;

namespace TaskChecklist.Models
{
    public partial class T01Job
    {
        public T01Job()
        {
            T02JobTasks = new HashSet<T02JobTask>();
        }

        public int Id { get; set; }
        public int JobTemplateId { get; set; }
        public string JobName { get; set; } = null!;
        public string? JobDesc { get; set; }
        public DateTime JobDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual T03Template JobTemplate { get; set; } = null!;
        public virtual ICollection<T02JobTask> T02JobTasks { get; set; }
    }
}

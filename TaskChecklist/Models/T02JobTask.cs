using System;
using System.Collections.Generic;

namespace TaskChecklist.Models
{
    public partial class T02JobTask
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskComments { get; set; }
        public bool TaskSkipped { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CreatedBy { get; set; }

        public virtual T01Job Job { get; set; } = null!;
        public virtual T04TemplateTask Task { get; set; } = null!;
    }
}

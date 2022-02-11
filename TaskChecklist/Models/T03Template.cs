using System;
using System.Collections.Generic;

namespace TaskChecklist.Models
{
    public partial class T03Template
    {
        public T03Template()
        {
            T01Jobs = new HashSet<T01Job>();
            T04TemplateTasks = new HashSet<T04TemplateTask>();
        }

        public int Id { get; set; }
        public string TemplateName { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<T01Job> T01Jobs { get; set; }
        public virtual ICollection<T04TemplateTask> T04TemplateTasks { get; set; }
    }
}

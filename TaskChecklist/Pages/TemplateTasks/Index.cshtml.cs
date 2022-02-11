#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist.Pages.TemplateTasks
{
    public class IndexModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public IndexModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public IList<T04TemplateTask> T04TemplateTask { get;set; }

        public async Task OnGetAsync()
        {
            T04TemplateTask = await _context.T04TemplateTasks
                .Include(t => t.Template).ToListAsync();
        }
    }
}

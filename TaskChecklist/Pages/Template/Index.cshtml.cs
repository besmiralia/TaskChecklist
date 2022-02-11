#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist.Pages.Template
{
    public class IndexModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public IndexModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public IList<T03Template> T03Template { get;set; }

        public async Task OnGetAsync()
        {
            T03Template = await _context.T03Templates.Include(x=>x.T04TemplateTasks).ToListAsync();
        }
    }
}

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist
{
    public class IndexModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public IndexModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public IList<T01Job> T01Job { get;set; }

        public async Task OnGetAsync()
        {
            T01Job = await _context.T01Jobs.Include(x=>x.JobTemplate).Include(x=>x.T02JobTasks).OrderByDescending(x=>x.Id).ToListAsync();
        }
    }
}

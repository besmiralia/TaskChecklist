#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist
{
    public class StartModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public StartModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public T01Job T01Job { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            T01Job = await _context.T01Jobs.Include(x => x.JobTemplate).Include(x => x.T02JobTasks).FirstOrDefaultAsync(m => m.Id == id);

            if (T01Job == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnGetTask(int id, int taskId)
        {
            var T02 = await _context.T02JobTasks.FirstOrDefaultAsync(x => x.Id == taskId && x.JobId == id);
            if (T02 == null)
            {
                return NotFound();
            }
            if (T02.StartDate == null) T02.StartDate = DateTime.Now;
            else T02.EndDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Start", new { id = id });
        }

        public async Task<IActionResult> OnGetSkipTask(int id, int taskId)
        {
            var T02 = await _context.T02JobTasks.FirstOrDefaultAsync(x => x.Id == taskId && x.JobId == id);
            if (T02 == null)
            {
                return NotFound();
            }
            if (T02.StartDate == null && T02.EndDate == null)
                T02.TaskSkipped = true;

            await _context.SaveChangesAsync();

            return RedirectToPage("Start", new { id = id });
        }

    }
}

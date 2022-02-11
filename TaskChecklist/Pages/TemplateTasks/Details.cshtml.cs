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
    public class DetailsModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public DetailsModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public T04TemplateTask T04TemplateTask { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            T04TemplateTask = await _context.T04TemplateTasks
                .Include(t => t.Template).FirstOrDefaultAsync(m => m.Id == id);

            if (T04TemplateTask == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

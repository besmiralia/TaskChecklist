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
    public class DetailsModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public DetailsModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public T03Template T03Template { get; set; }
        public List<T04TemplateTask> T04Tasks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            T03Template = await _context.T03Templates.FirstOrDefaultAsync(m => m.Id == id);

            if (T03Template == null)
            {
                return NotFound();
            }
            T04Tasks = _context.T04TemplateTasks.Where(x => x.TemplateId == id).ToList();

            return Page();
        }
    }
}

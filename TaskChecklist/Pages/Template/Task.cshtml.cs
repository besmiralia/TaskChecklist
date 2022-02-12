using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist.Pages.Template
{
    public class TaskModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public TaskModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }
        public T03Template T03Template { get; set; }
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
            ViewData["TemplateName"] = T03Template.TemplateName;
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostUp(int id, int taskId)
        {
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

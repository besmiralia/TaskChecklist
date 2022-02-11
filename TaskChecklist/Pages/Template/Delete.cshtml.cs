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
    public class DeleteModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public DeleteModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            T03Template = await _context.T03Templates.FindAsync(id);

            if (T03Template != null)
            {
                _context.T03Templates.Remove(T03Template);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

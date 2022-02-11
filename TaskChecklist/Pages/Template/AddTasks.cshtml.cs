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

namespace TaskChecklist.Pages.Template
{
    public class AddTasks : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public AddTasks(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var T03Template = await _context.T03Templates.FirstOrDefaultAsync(m => m.Id == id);

            if (T03Template == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public T03Template T03Template { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            T03Template.Active = true;
            _context.T03Templates.Add(T03Template);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

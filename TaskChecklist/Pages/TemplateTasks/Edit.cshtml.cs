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

namespace TaskChecklist.Pages.TemplateTasks
{
    public class EditModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public EditModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["TemplateId"] = new SelectList(_context.T03Templates, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(T04TemplateTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T04TemplateTaskExists(T04TemplateTask.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool T04TemplateTaskExists(int id)
        {
            return _context.T04TemplateTasks.Any(e => e.Id == id);
        }
    }
}

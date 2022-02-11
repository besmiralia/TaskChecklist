#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskChecklist.Models;

namespace TaskChecklist.Pages.TemplateTasks
{
    public class CreateModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public CreateModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TemplateId"] = new SelectList(_context.T03Templates, "Id", "TemplateName");
            return Page();
        }

        [BindProperty]
        public T04TemplateTask T04TemplateTask { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }*/
            T04TemplateTask.UpdatedAt = DateTime.Now;
            T04TemplateTask.UpdatedBy = User.Identity.Name;
            _context.T04TemplateTasks.Add(T04TemplateTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

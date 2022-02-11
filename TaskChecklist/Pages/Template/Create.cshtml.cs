#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskChecklist.Models;

namespace TaskChecklist.Pages.Template
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

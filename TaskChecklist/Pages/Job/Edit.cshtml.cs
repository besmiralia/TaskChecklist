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
    public class EditModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public EditModel(TaskChecklist.Models.DBTasksContext context)
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

            T01Job = await _context.T01Jobs.FirstOrDefaultAsync(m => m.Id == id);

            if (T01Job == null)
            {
                return NotFound();
            }
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

            _context.Attach(T01Job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T01JobExists(T01Job.Id))
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

        private bool T01JobExists(int id)
        {
            return _context.T01Jobs.Any(e => e.Id == id);
        }
    }
}

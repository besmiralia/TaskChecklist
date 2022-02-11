#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskChecklist.Models;

namespace TaskChecklist
{
    public class DetailsModel : PageModel
    {
        private readonly TaskChecklist.Models.DBTasksContext _context;

        public DetailsModel(TaskChecklist.Models.DBTasksContext context)
        {
            _context = context;
        }

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
    }
}

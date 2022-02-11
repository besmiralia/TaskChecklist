#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskChecklist.Models;

namespace TaskChecklist
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
        public T01Job T01Job { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }
            */
            T01Job.JobDate = DateTime.Now;
            T01Job.CreatedAt = DateTime.Now;
            T01Job.CreatedBy = User.Identity.Name;

            _context.T01Jobs.Add(T01Job);

            await _context.SaveChangesAsync();

            var T04s = _context.T04TemplateTasks.Where(x => x.TemplateId == T01Job.JobTemplateId).OrderBy(x=>x.TaskOrder);
            foreach (var t04 in T04s)
            {
                var T02 = new T02JobTask
                {
                    JobId = T01Job.Id,
                    TaskId = t04.Id,
                    TaskName = t04.TaskName
                };
                _context.T02JobTasks.Add(T02);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Start", new { id = T01Job.Id });
        }
    }
}

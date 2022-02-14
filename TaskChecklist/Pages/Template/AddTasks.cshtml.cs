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
            T04TemplateTask = new T04TemplateTask();
            T04TemplateTask.TaskOrder = 0;
            return Page();
        }
        [BindProperty]
        public T04TemplateTask T04TemplateTask { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id)
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }*/
            if(T04TemplateTask.TaskOrder <= 0)
            {
                var max = _context.T04TemplateTasks.Where(m => m.TemplateId == id).Count();
                T04TemplateTask.TaskOrder = max + 1;
            }
            else
            {
                var TO4s = _context.T04TemplateTasks.Where(x => x.TemplateId == id && x.TaskOrder >= T04TemplateTask.TaskOrder).ToList();
                var i = 1;
                foreach (var t04 in TO4s)
                {
                    t04.TaskOrder = T04TemplateTask.TaskOrder + i++;
                }
            }
            T04TemplateTask.TemplateId = id;
            T04TemplateTask.UpdatedAt = DateTime.Now;
            T04TemplateTask.UpdatedBy = User.Identity.Name;
            _context.T04TemplateTasks.Add(T04TemplateTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { id = id });
        }
        public async Task<IActionResult> OnGetMove(int id, int taskId, string move)
        {
            var T04 = _context.T04TemplateTasks.Find(taskId);
            if(T04 == null) return NotFound();
            if (move == "up")
            {
                if (T04.TaskOrder > 1)
                {
                    T04.TaskOrder--;
                }
            }
            else
            {
                T04.TaskOrder++;
            }
            var moveT04 = _context.T04TemplateTasks.FirstOrDefault(x => x.TemplateId == id && x.Id != taskId && x.TaskOrder == T04.TaskOrder);
            if (moveT04 != null)
            {
                if (move == "up") moveT04.TaskOrder++; else moveT04.TaskOrder--;
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { id = id });
        }

        public async Task<IActionResult> OnGetDelete(int id, int taskId)
        {
            var T04 = _context.T04TemplateTasks.Find(taskId);
            if (T04 != null)
            {
                _context.T04TemplateTasks.Remove(T04);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Details", new { id = id });
        }
    }
}

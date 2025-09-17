using HotelLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorServicePersonal.Pages;

public class Cleaning : PageModel
{

    private readonly ITaskService _taskService;
    public IEnumerable<HotelLibrary.Models.Task> Tasks { get; set; }
    public Cleaning(ITaskService taskService)
    {
        this._taskService = taskService;
        
    }
    
    public async Task OnGetAsync()
    {
       
        Tasks = await _taskService.GetAllTasksAsync();
 
        Tasks = Tasks.Where(s => s.Type == "Clean" && (s.Status == "New" || s.Status== "In Progress"));
    }
    
    public async Task<IActionResult> OnPostAsync(int taskId, string status, string description)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);
        if (task == null)
        {
            // Håndter feil...
            return Page();
        }

        task.Status = status;
        task.Description = description;
        await _taskService.UpdateTaskAsync(task);
        return RedirectToPage();  
    }
}

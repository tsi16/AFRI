using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afri.Controllers;

public class StudentProgressController : Controller
{
    private readonly AFRILEARNContext _context;
    private readonly CurrentUser _currentUser;

    public StudentProgressController(AFRILEARNContext context, CurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Index()
    {
        int userId = _currentUser.GetUserId();
        if (userId == 0)
            return RedirectToAction("Signin", "Users");

        var progress = await _context.StudentProgresses
            .Include(p => p.Lesson).ThenInclude(l => l!.Topic).ThenInclude(t => t!.Subject)
            .Where(p => p.StudentId == userId)
            .OrderByDescending(p => p.LastUpdated)
            .ToListAsync();

        ViewBag.StudentName = _currentUser.GetUserName();
        return View(progress);
    }
}

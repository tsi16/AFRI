using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afri.Controllers;

public class LeaderboardController : Controller
{
    private readonly AFRILEARNContext _context;
    private readonly CurrentUser _currentUser;

    public LeaderboardController(AFRILEARNContext context, CurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Index()
    {
        int userId = _currentUser.GetUserId();
        if (userId == 0)
            return RedirectToAction("Signin", "Users");

        var scores = await _context.LeaderboardScores
            .Include(s => s.Student)
            .OrderByDescending(s => s.TotalPoints)
            .Take(50)
            .ToListAsync();

        ViewBag.CurrentUserId = userId;
        return View(scores);
    }
}

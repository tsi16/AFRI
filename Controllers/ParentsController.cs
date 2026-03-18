using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Afri.Controllers
{
    public class ParentsController : Controller
    {
        private readonly AFRILEARNContext _context;
        private readonly CurrentUser _currentUser;

        public ParentsController(AFRILEARNContext context, CurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!_currentUser.IsParent())
            {
                return RedirectToAction("Index", "Home");
            }

            int parentId = _currentUser.GetUserId();
            var children = await _context.Users
                .Include(u => u.StudentActivityLogs.OrderByDescending(al => al.ActivityDate).Take(5))
                .Include(u => u.StudentQuizAttempts).ThenInclude(qa => qa.Quiz)
                .Include(u => u.StudentProgresses).ThenInclude(sp => sp.Lesson).ThenInclude(l => l.Topic).ThenInclude(t => t.Subject)
                .Where(u => u.ParentId == parentId)
                .ToListAsync();

            return View(children);
        }
    }
}

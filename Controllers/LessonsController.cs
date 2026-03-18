using Afri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Afri.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AFRILEARNContext _context;

        public LessonsController(AFRILEARNContext context)
        {
            _context = context;
        }

        // GET: Lessons/ViewLesson/5
        public async Task<IActionResult> ViewLesson(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Topic)
                    .ThenInclude(t => t.Subject)
                .Include(l => l.Topic)
                    .ThenInclude(t => t.Lessons) // For navigation sidebar
                .Include(l => l.Topic)
                    .ThenInclude(t => t.Quizzes) // To check if topic has quiz
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }


        public async Task<IActionResult> Index()
        {
            var translations = await _context.LessonTranslations
                .Include(l => l.Language)
                .Include(l => l.Lesson)
                .OrderByDescending(l => l.CreatedDate)
                .ToListAsync();
            return View(translations);
        }

        public IActionResult CreateTranslation()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTranslation(LessonTranslation model)
        {
            model.CreatedDate = DateTime.Now;
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- DOWNLOAD FUNCTIONALITY ---

        [HttpPost]
        public async Task<IActionResult> DownloadLesson(int lessonId, int studentId)
        {
            var download = new DownloadedLesson
            {
                LessonId = lessonId,
                StudentId = studentId,
                DownloadDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(30), // Example expiry
                IsSynced = false
            };

            _context.DownloadedLessons.Add(download);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Lesson marked for offline use." });
        }

        // --- OFFLINE SYNC LOGGING ---

        [HttpPost]
        public async Task<IActionResult> LogSync(int studentId, string deviceInfo)
        {
            var log = new OfflineSyncLog
            {
                StudentId = studentId,
                SyncDate = DateTime.Now,
                DeviceInfo = deviceInfo
            };

            _context.OfflineSyncLogs.Add(log);

            // Mark all previous downloads as synced
            var unsynced = _context.DownloadedLessons.Where(d => d.StudentId == studentId && d.IsSynced == false);
            foreach (var item in unsynced) item.IsSynced = true;

            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}

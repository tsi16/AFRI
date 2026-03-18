//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Afri.Models;

//namespace Afri.Controllers
//{
//    public class SubjectsController : Controller
//    {
//        private readonly AFRILEARNContext _context;

//        public SubjectsController(AFRILEARNContext context)
//        {
//            _context = context;
//        }

//        // GET: Subjects
//        public async Task<IActionResult> Index()
//        {
//            var aFRILEARNContext = _context.Subjects.Include(s => s.GradeLevel);
//           // var subjects = await _context.Subjects.Include(s => s.GradeLevel).ToListAsync();

//            // Fetch all available grades from the GradeLevels table for the filter buttons
//            ViewBag.Grades = await _context.GradeLevels.OrderBy(g => g.Id).ToListAsync();
//            return View(await aFRILEARNContext.ToListAsync());
//        }

//        // GET: Subjects/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var subject = await _context.Subjects
//                .Include(s => s.GradeLevel)
//                .Include(s => s.Topics).ThenInclude(x=>x.Lessons)
//                .Include(s => s.Topics).ThenInclude(x=>x.Quizzes)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (subject == null)
//            {
//                return NotFound();
//            }

//            return View(subject);
//        }

//        // GET: Subjects/Create
//        public IActionResult Create()
//        {
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name");
//            return View();
//        }

//        // POST: Subjects/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Name,Code,GradeLevelId,Description,IsCore")] Subject subject)
//        {

//            ModelState.Remove("GradeLevel");

//            if (ModelState.IsValid)
//            {
//                subject.CreatedDate = DateTime.Now;
//                subject.ModifiedDate = DateTime.Now;

//                _context.Add(subject);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }


//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);
//            return View(subject);
//        }
//        // GET: Subjects/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var subject = await _context.Subjects.FindAsync(id);
//            if (subject == null)
//            {
//                return NotFound();
//            }
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);
//            return View(subject);
//        }

//        // POST: Subjects/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,GradeLevelId,Description,IsCore,CreatedDate,ModifiedDate")] Subject subject)
//        {
//            if (id != subject.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(subject);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SubjectExists(subject.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);
//            return View(subject);
//        }

//        // GET: Subjects/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var subject = await _context.Subjects
//                .Include(s => s.GradeLevel)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (subject == null)
//            {
//                return NotFound();
//            }

//            return View(subject);
//        }

//        // POST: Subjects/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var subject = await _context.Subjects.FindAsync(id);
//            if (subject != null)
//            {
//                _context.Subjects.Remove(subject);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool SubjectExists(int id)
//        {
//            return _context.Subjects.Any(e => e.Id == id);
//        }
//        // POST: Subjects/SaveTopic
//        [HttpPost]
//        [ValidateAntiForgeryToken] // Protects against CSRF attacks
//        public async Task<IActionResult> SaveTopic(Topic topic)
//        {
//            if (topic.Id == 0)
//            {
//                // New Topic
//                _context.Topics.Add(topic);
//            }
//            else
//            {
//                // Update existing: Load it from DB first to keep metadata safe
//                var dbTopic = await _context.Topics.FindAsync(topic.Id);
//                if (dbTopic == null) return NotFound();

//                // Only update the fields the user is allowed to change
//                dbTopic.Title = topic.Title;
//                // dbTopic.OrderNumber = topic.OrderNumber; // Use this if you add sorting later

//                _context.Update(dbTopic);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction("Details", new { id = topic.SubjectId });
//        }

//        [HttpPost]
//        public async Task<IActionResult> SaveLesson(Lesson lesson, int subjectId)
//        {
//            if (lesson.Id == 0)
//            {
//                _context.Lessons.Add(lesson);
//            }
//            else
//            {
//                // 1. Find the existing lesson in the database
//                var existingLesson = await _context.Lessons.FindAsync(lesson.Id);
//                if (existingLesson != null)
//                {
//                    // 2. ONLY update the fields that were in the modal
//                    existingLesson.Title = lesson.Title;
//                    // existingLesson.OrderNumber = lesson.OrderNumber; // Add this if you add ordering

//                    _context.Update(existingLesson);
//                }
//            }
//            await _context.SaveChangesAsync();
//            return RedirectToAction("Details", new { id = subjectId });
//        }
//        public async Task<IActionResult> DeleteTopic(int id)
//        {
//            // 1. Fetch the topic and include its lessons
//            var topic = await _context.Topics
//                .Include(t => t.Lessons)
//                .FirstOrDefaultAsync(t => t.Id == id);

//            if (topic != null)
//            {
//                int subjectId = topic.SubjectId;

//                // 2. Loop through each lesson and clean up its questions first 
//                // (This prevents the error you had in the previous step!)
//                foreach (var lesson in topic.Lessons)
//                {
//                    var relatedQuestions = _context.StudentQuestions.Where(q => q.LessonId == lesson.Id);
//                    _context.StudentQuestions.RemoveRange(relatedQuestions);
//                }

//                // 3. Remove all Lessons belonging to this Topic
//                _context.Lessons.RemoveRange(topic.Lessons);

//                // 4. Finally, remove the Topic itself
//                _context.Topics.Remove(topic);

//                await _context.SaveChangesAsync();

//                return RedirectToAction("Details", new { id = subjectId });
//            }

//            return NotFound();
//        }
//        public async Task<IActionResult> DeleteLesson(int id)
//        {
//            var lesson = await _context.Lessons
//                .Include(l => l.Topic)
//                .FirstOrDefaultAsync(m => m.Id == id);

//            if (lesson != null)
//            {
//                int subjectId = lesson.Topic.SubjectId;

//                // 1. Find and remove all questions related to this lesson first
//                var relatedQuestions = _context.StudentQuestions.Where(q => q.LessonId == id);
//                _context.StudentQuestions.RemoveRange(relatedQuestions);

//                // 2. Now delete the lesson
//                _context.Lessons.Remove(lesson);

//                await _context.SaveChangesAsync();
//                return RedirectToAction("Details", new { id = subjectId });
//            }
//            return NotFound();
//        }

//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Afri.Models;

namespace Afri.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly AFRILEARNContext _context;

        public SubjectsController(AFRILEARNContext context)
        {
            _context = context;
        }

        // =========================
        // SUBJECT LIST
        // =========================
        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects
                .Include(s => s.GradeLevel)
                .ToListAsync();

            ViewBag.Grades = await _context.GradeLevels
                .OrderBy(g => g.Id)
                .ToListAsync();

            return View(subjects);
        }

        // =========================
        // SUBJECT DETAILS
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var subject = await _context.Subjects
                .Include(s => s.GradeLevel)
                .Include(s => s.Topics).ThenInclude(t => t.Lessons)
                .Include(s => s.Topics).ThenInclude(t => t.Quizzes)
                .Include(s => s.Topics).ThenInclude(t => t.TopicMaterials)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null) return NotFound();

            return View(subject);
        }

        // =========================
        // CREATE SUBJECT
        // =========================
        public IActionResult Create()
        {
            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,GradeLevelId,Description,IsCore")] Subject subject)
        {
            ModelState.Remove("GradeLevel");

            if (ModelState.IsValid)
            {
                subject.CreatedDate = DateTime.Now;
                subject.ModifiedDate = DateTime.Now;

                _context.Add(subject);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);
            return View(subject);
        }

        // =========================
        // EDIT SUBJECT
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);

            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,GradeLevelId,Description,IsCore,CreatedDate,ModifiedDate")] Subject subject)
        {
            if (id != subject.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", subject.GradeLevelId);
            return View(subject);
        }

        // =========================
        // DELETE SUBJECT
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var subject = await _context.Subjects
                .Include(s => s.GradeLevel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subject == null) return NotFound();

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // SAVE TOPIC
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTopic(Topic topic)
        {
            if (topic.Id == 0)
            {
                _context.Topics.Add(topic);
            }
            else
            {
                var dbTopic = await _context.Topics.FindAsync(topic.Id);
                if (dbTopic == null) return NotFound();

                dbTopic.Title = topic.Title;

                _context.Update(dbTopic);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = topic.SubjectId });
        }

        // =========================
        // SAVE LESSON
        // =========================
        [HttpPost]
        public async Task<IActionResult> SaveLesson(Lesson lesson, int subjectId)
        {
            if (lesson.Id == 0)
            {
                _context.Lessons.Add(lesson);
            }
            else
            {
                var existingLesson = await _context.Lessons.FindAsync(lesson.Id);

                if (existingLesson != null)
                {
                    existingLesson.Title = lesson.Title;

                    _context.Update(existingLesson);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = subjectId });
        }

        // =========================
        // DELETE TOPIC
        // =========================
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.Lessons)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null) return NotFound();

            int subjectId = topic.SubjectId;

            foreach (var lesson in topic.Lessons)
            {
                var questions = _context.StudentQuestions.Where(q => q.LessonId == lesson.Id);
                _context.StudentQuestions.RemoveRange(questions);
            }

            _context.Lessons.RemoveRange(topic.Lessons);
            _context.Topics.Remove(topic);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = subjectId });
        }

        // =========================
        // DELETE LESSON
        // =========================
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lesson == null) return NotFound();

            int subjectId = lesson.Topic.SubjectId;

            var questions = _context.StudentQuestions.Where(q => q.LessonId == id);
            _context.StudentQuestions.RemoveRange(questions);

            _context.Lessons.Remove(lesson);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = subjectId });
        }

        // =========================
        // DOWNLOAD LESSON (OFFLINE)
        // =========================
        [HttpPost]
        public IActionResult DownloadLesson(int lessonId, int languageId)
        {
            try
            {
                var lesson = _context.Lessons.FirstOrDefault(l => l.Id == lessonId);

                if (lesson == null)
                    return NotFound();

                var content = new StringBuilder();

                content.AppendLine($"AfriLearn - {lesson.Title}");
                content.AppendLine("======================");
                content.AppendLine($"Downloaded: {DateTime.Now}");
                content.AppendLine();
                content.AppendLine("Offline lesson content placeholder");

                var bytes = Encoding.UTF8.GetBytes(content.ToString());

                var fileName = $"{lesson.Title.Replace(" ", "_")}.txt";

                return File(bytes, "text/plain", fileName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // =========================
        // GET TRANSLATIONS
        // =========================
        [HttpPost]
        public IActionResult GetTranslations(int lessonId)
        {
            try
            {
                var translations = _context.LessonTranslations
                    .Where(t => t.LessonId == lessonId)
                    .Select(t => new
                    {
                        t.LanguageId,
                        t.Language.Name,
                        t.TranslatedContent,
                        t.IsHumanValidated
                    })
                    .ToList();

                return Json(translations);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // =========================
        // REMOVE OFFLINE DOWNLOAD
        // =========================
        [HttpPost]
        public IActionResult RemoveDownload(int lessonId)
        {
            try
            {
                TempData["Success"] = "Offline lesson removed";
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // =========================
        // EXISTS CHECK
        // =========================
        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
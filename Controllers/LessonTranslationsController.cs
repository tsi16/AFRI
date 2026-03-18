using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Afri.Controllers
{
    public class LessonTranslationsController : Controller
    {
        private readonly AFRILEARNContext _context;
        private readonly CurrentUser _currentUser;
        private readonly string _geminiApiKey = "AIzaSyCwS6PQ1yfoqKbt-9OIHZwyCJYsj63x6nQ";

        public LessonTranslationsController(AFRILEARNContext context, CurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> GetAiTutorAssistant(int lessonId, int languageId)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(lessonId);
                var language = await _context.Languages.FindAsync(languageId);

                if (lesson == null || language == null) return Json(new { success = false });

                // 1. Generate AI Chat Explanation
                string prompt = $"You are an Afri tutor. In {language.Name}, provide a friendly and deep explanation of the lesson: {lesson.Title}. Summarize the key points clearly.";
                string aiText = await CallGemini(prompt);

                // 2. Generate Voice URL (Placeholder for your TTS service)
                // In a real project, you'd send 'aiText' to an API like Google Cloud TTS or Azure Voice
                string voiceUrl = $"/audio/tutor_{lessonId}_{languageId}.mp3";

                // 3. Save to LessonTranslation (Using your EXACT fields)
                var translation = new LessonTranslation
                {
                    LessonId = lessonId,
                    LanguageId = languageId,
                    TranslatedContent = aiText,       // The Chat Explanation
                    VoiceExplanationUrl = voiceUrl,   // The Voice Link
                    IsHumanValidated = false,
                    CreatedDate = DateTime.Now
                };

                _context.LessonTranslations.Add(translation);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    content = translation.TranslatedContent,
                    voice = translation.VoiceExplanationUrl
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveForOffline(int lessonId)
        {
            int userId = _currentUser.GetUserId();

            // Save to DownloadedLesson (Exact fields)
            var download = new DownloadedLesson
            {
                StudentId = userId,
                LessonId = lessonId,
                DownloadDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(30),
                IsSynced = false
            };
            _context.DownloadedLessons.Add(download);

            // Log the Sync (Exact fields)
            var log = new OfflineSyncLog
            {
                StudentId = userId,
                SyncDate = DateTime.Now,
                DeviceInfo = Request.Headers["User-Agent"].ToString()
            };
            _context.OfflineSyncLogs.Add(log);

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            return Json(await _context.Languages.Select(l => new { l.Id, l.Name }).ToListAsync());
        }

        private async Task<string> CallGemini(string prompt)
        {
            using var client = new HttpClient();
            var endpoint = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={_geminiApiKey}";
            var payload = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
            var response = await client.PostAsync(endpoint, new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
        }

        [HttpPost]
        public async Task<IActionResult> Download(int lessonId)
        {
            int userId = _currentUser.GetUserId();
            if (userId == 0) return Json(new { success = false });

            // Check if already downloaded
            var exists = await _context.DownloadedLessons
                .AnyAsync(d => d.StudentId == userId && d.LessonId == lessonId);

            if (!exists)
            {
                var download = new DownloadedLesson
                {
                    StudentId = userId,
                    LessonId = lessonId,
                    DownloadDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    IsSynced = false // Marked as false until the Sync log is created
                };
                _context.DownloadedLessons.Add(download);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        // POST: /LessonTranslations/Sync
        [HttpPost]
        public async Task<IActionResult> Sync()
        {
            int userId = _currentUser.GetUserId();
            if (userId == 0) return Ok();

            // Create Sync Log (Using your exact Model fields)
            var log = new OfflineSyncLog
            {
                StudentId = userId,
                SyncDate = DateTime.Now,
                DeviceInfo = Request.Headers["User-Agent"].ToString()
            };
            _context.OfflineSyncLogs.Add(log);

            // Update the DownloadedLesson sync status
            var unsynced = _context.DownloadedLessons.Where(d => d.StudentId == userId && d.IsSynced == false);
            foreach (var item in unsynced) item.IsSynced = true;

            await _context.SaveChangesAsync();
            return Ok();
        }





    }
}


//namespace Afri.Controllers
//{
//    public class LessonTranslationsController : Controller
//    {
//        private readonly AFRILEARNContext _context;

//        public LessonTranslationsController(AFRILEARNContext context)
//        {
//            _context = context;
//        }

//        // GET: LessonTranslations
//        public async Task<IActionResult> Index()
//        {
//            var aFRILEARNContext = _context.LessonTranslations.Include(l => l.Language).Include(l => l.Lesson);
//            return View(await aFRILEARNContext.ToListAsync());
//        }

//        // GET: LessonTranslations/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var lessonTranslation = await _context.LessonTranslations
//                .Include(l => l.Language)
//                .Include(l => l.Lesson)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (lessonTranslation == null)
//            {
//                return NotFound();
//            }

//            return View(lessonTranslation);
//        }

//        // GET: LessonTranslations/Create
//        public IActionResult Create()
//        {
//            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id");
//            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id");
//            return View();
//        }

//        // POST: LessonTranslations/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,LessonId,LanguageId,TranslatedContent,VoiceExplanationUrl,IsHumanValidated,CreatedDate,ModifiedDate")] LessonTranslation lessonTranslation)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(lessonTranslation);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", lessonTranslation.LanguageId);
//            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTranslation.LessonId);
//            return View(lessonTranslation);
//        }

//        // GET: LessonTranslations/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var lessonTranslation = await _context.LessonTranslations.FindAsync(id);
//            if (lessonTranslation == null)
//            {
//                return NotFound();
//            }
//            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", lessonTranslation.LanguageId);
//            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTranslation.LessonId);
//            return View(lessonTranslation);
//        }

//        // POST: LessonTranslations/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,LessonId,LanguageId,TranslatedContent,VoiceExplanationUrl,IsHumanValidated,CreatedDate,ModifiedDate")] LessonTranslation lessonTranslation)
//        {
//            if (id != lessonTranslation.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(lessonTranslation);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LessonTranslationExists(lessonTranslation.Id))
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
//            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", lessonTranslation.LanguageId);
//            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTranslation.LessonId);
//            return View(lessonTranslation);
//        }

//        // GET: LessonTranslations/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var lessonTranslation = await _context.LessonTranslations
//                .Include(l => l.Language)
//                .Include(l => l.Lesson)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (lessonTranslation == null)
//            {
//                return NotFound();
//            }

//            return View(lessonTranslation);
//        }

//        // POST: LessonTranslations/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var lessonTranslation = await _context.LessonTranslations.FindAsync(id);
//            if (lessonTranslation != null)
//            {
//                _context.LessonTranslations.Remove(lessonTranslation);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]
//        public async Task<IActionResult> GetAiTranslation(int lessonId, int languageId)
//        {
//            try
//            {
//                int userId = _currentUser.GetUserId();
//                if (userId == 0) return Json(new { success = false, message = "Please login." });

//                // Fetch data from DB (No hardcoding)
//                var lesson = await _context.Lessons.FindAsync(lessonId);
//                var language = await _context.Languages.FindAsync(languageId);

//                if (lesson == null || language == null)
//                    return Json(new { success = false, message = "Lesson or Language not found." });

//                // Check if translation exists
//                var translation = await _context.LessonTranslations
//                    .FirstOrDefaultAsync(lt => lt.LessonId == lessonId && lt.LanguageId == languageId);

//                if (translation == null)
//                {
//                    // Call Gemini AI
//                    string prompt = $"You are an AfriLearn tutor. Translate the following lesson into {language.Name}. \nTitle: {lesson.Title} \nContent: {lesson.BaseLanguageContent}";
//                    string aiResponse = await CallGemini(prompt);

//                    translation = new LessonTranslation
//                    {
//                        LessonId = lessonId,
//                        LanguageId = languageId,
//                        TranslatedTitle = lesson.Title + " (" + language.Name + ")", // As requested
//                        TranslatedSummary = "AI Generated Summary for " + lesson.Title,
//                        TranslatedContent = aiResponse,
//                        IsHumanValidated = false,
//                        IsActive = true,
//                        IsDeleted = false,
//                        CreatedDate = DateTime.Now,
//                        CreatedBy = userId.ToString()
//                    };

//                    _context.LessonTranslations.Add(translation);
//                    await _context.SaveChangesAsync();
//                }

//                return Json(new
//                {
//                    success = true,
//                    title = translation.TranslatedTitle,
//                    content = translation.TranslatedContent
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> DownloadLesson(int lessonId)
//        {
//            int userId = _currentUser.GetUserId();
//            var download = new DownloadedLesson
//            {
//                StudentId = userId,
//                LessonId = lessonId,
//                DownloadDate = DateTime.Now,
//                ExpiryDate = DateTime.Now.AddDays(30),
//                IsSynced = false
//            };
//            _context.DownloadedLessons.Add(download);
//            await _context.SaveChangesAsync();
//            return Json(new { success = true });
//        }

//        [HttpPost]
//        public async Task<IActionResult> SyncDevice(int recordsCount)
//        {
//            int userId = _currentUser.GetUserId();
//            var syncLog = new OfflineSyncLog
//            {
//                StudentId = userId,
//                SyncDate = DateTime.Now,
//                SyncedRecordsCount = recordsCount, // Field from your design
//                DeviceInfo = Request.Headers["User-Agent"].ToString()
//            };
//            _context.OfflineSyncLogs.Add(syncLog);

//            var unsynced = _context.DownloadedLessons.Where(d => d.StudentId == userId && d.IsSynced == false);
//            foreach (var d in unsynced) d.IsSynced = true;

//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        private async Task<string> CallGemini(string prompt)
//        {
//            using var client = new HttpClient();
//            var endpoint = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={_geminiApiKey}";
//            var payload = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
//            var response = await client.PostAsync(endpoint, new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));
//            var result = await response.Content.ReadAsStringAsync();
//            using var doc = JsonDocument.Parse(result);
//            return doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
//        }

//        // Helper to get languages for the modal
//        public async Task<JsonResult> GetAvailableLanguages()
//        {
//            var languages = await _context.Languages.Select(l => new { l.Id, l.Name }).ToListAsync();
//            return Json(languages);
//        }
//    }
//}

using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Afri.Controllers
{
    public class AiinteractionFeedbacksController : Controller
    {
        private readonly AFRILEARNContext _context;
        private readonly CurrentUser _currentUser;

        // Consider moving this to configuration (appsettings) — kept inline to preserve original intent
        private readonly string _geminiApiKey = "AIzaSyCwS6PQ1yfoqKbt-9OIHZwyCJYsj63x6nQ";

        public AiinteractionFeedbacksController(AFRILEARNContext context, CurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: AiinteractionFeedbacks
        public async Task<IActionResult> Index()
        {
            var aFRILEARNContext = _context.AiinteractionFeedbacks.Include(a => a.Airesponse);
            return View(await aFRILEARNContext.ToListAsync());
        }

        // GET: AiinteractionFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var aiinteractionFeedback = await _context.AiinteractionFeedbacks
                .Include(a => a.Airesponse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aiinteractionFeedback == null) return NotFound();

            return View(aiinteractionFeedback);
        }

        // GET: AiinteractionFeedbacks/Create
        public IActionResult Create()
        {
            ViewData["AiresponseId"] = new SelectList(_context.Airesponses, "Id", "ResponseText");
            return View();
        }

        // POST: AiinteractionFeedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AiresponseId,Rating,Comments,CreatedDate")] AiinteractionFeedback aiinteractionFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aiinteractionFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AiresponseId"] = new SelectList(_context.Airesponses, "Id", "ResponseText", aiinteractionFeedback.AiresponseId);
            return View(aiinteractionFeedback);
        }

        // GET: AiinteractionFeedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var aiinteractionFeedback = await _context.AiinteractionFeedbacks.FindAsync(id);
            if (aiinteractionFeedback == null) return NotFound();

            ViewData["AiresponseId"] = new SelectList(_context.Airesponses, "Id", "ResponseText", aiinteractionFeedback.AiresponseId);
            return View(aiinteractionFeedback);
        }

        // POST: AiinteractionFeedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AiresponseId,Rating,Comments,CreatedDate")] AiinteractionFeedback aiinteractionFeedback)
        {
            if (id != aiinteractionFeedback.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aiinteractionFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AiinteractionFeedbackExists(aiinteractionFeedback.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AiresponseId"] = new SelectList(_context.Airesponses, "Id", "ResponseText", aiinteractionFeedback.AiresponseId);
            return View(aiinteractionFeedback);
        }

        // GET: AiinteractionFeedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var aiinteractionFeedback = await _context.AiinteractionFeedbacks
                .Include(a => a.Airesponse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aiinteractionFeedback == null) return NotFound();

            return View(aiinteractionFeedback);
        }

        // POST: AiinteractionFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aiinteractionFeedback = await _context.AiinteractionFeedbacks.FindAsync(id);
            if (aiinteractionFeedback != null)
            {
                _context.AiinteractionFeedbacks.Remove(aiinteractionFeedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AiinteractionFeedbackExists(int id)
        {
            return _context.AiinteractionFeedbacks.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AskQuestion(int lessonId, string questionText, string forcedLanguage)
        {
            try
            {
                int userId = _currentUser.GetUserId();
                if (userId == 0) return Json(new { success = false, message = "Please login first." });

                var lesson = await _context.Lessons
                    .Include(l => l.Topic).ThenInclude(t => t.Subject)
                    .FirstOrDefaultAsync(l => l.Id == lessonId);

                // Use the language selected by the student in the popup
                string languageToUse = forcedLanguage ?? "Amharic";

                // Call AI with the selected language
                string aiAnswer = await GetGeminiResponse(questionText, lesson, languageToUse);

                // Save Question
                var studentQuestion = new StudentQuestion
                {
                    LessonId = lessonId,
                    SubjectId = lesson?.Topic?.SubjectId ?? 0,
                    QuestionText = questionText,
                    CreatedDate = DateTime.Now,
                    StudentId = userId
                };
                _context.StudentQuestions.Add(studentQuestion);
                await _context.SaveChangesAsync();

                // Save Response
                var aiResponse = new Airesponse
                {
                    StudentQuestionId = studentQuestion.Id,
                    ResponseText = aiAnswer,
                    CreatedDate = DateTime.Now
                };
                _context.Airesponses.Add(aiResponse);
                await _context.SaveChangesAsync();

                return Json(new { success = true, answer = aiAnswer, aiResponseId = aiResponse.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "AI Error: " + ex.Message });
            }
        }

        private async Task<string> GetGeminiResponse(string question, Lesson lesson, string language)
        {
            try
            {
                using var client = new HttpClient();
                var endpoint = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_geminiApiKey}";

                var requestData = new
                {
                    contents = new[]
                    {
                        new {
                            parts = new[] {
                                new { text = $"You are an expert AfriLearn AI Tutor strictly following the Ethiopian educational curriculum. Your explanations must be highly relevant to Ethiopian students, contextualized with local Ethiopian examples, culture, and educational standards. Do not include content outside the Ethiopian curriculum. Explain this in {language}: {question} (Lesson Context: {lesson?.Title} in {lesson?.Topic?.Subject?.Name})" }
                            }
                        }
                    }
                };

                var jsonPayload = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpoint, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

                // defensive parsing — try to read candidates -> content -> parts -> text
                if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var first = candidates[0];
                    if (first.TryGetProperty("content", out var contentEl) &&
                        contentEl.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0)
                    {
                        var part = parts[0];
                        if (part.TryGetProperty("text", out var textEl))
                        {
                            return textEl.GetString() ?? "No answer found.";
                        }
                    }
                }

                return "The AI is busy. Please try again.";
            }
            catch (Exception ex)
            {
                return "System Error: " + ex.Message;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAiExplanation(string question, string language)
        {
            try
            {
                string result = await GetGeminiResponse(question, null, language);
                return Content(result);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChatHistory(int lessonId)
        {
            try
            {
                int userId = _currentUser.GetUserId();
                if (userId == 0) return Json(new { success = false, message = "Please login first." });

                var history = await _context.StudentQuestions
                    .Include(q => q.Airesponses)
                    .Where(q => q.StudentId == userId && q.LessonId == lessonId)
                    .OrderBy(q => q.CreatedDate)
                    .Select(q => new
                    {
                        question = q.QuestionText,
                        answer = q.Airesponses.FirstOrDefault().ResponseText,
                        date = q.CreatedDate
                    })
                    .ToListAsync();

                return Json(new { success = true, history });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback(int responseId, int rating, string comments)
        {
            var feedback = new AiinteractionFeedback
            {
                AiresponseId = responseId,
                Rating = rating,
                Comments = comments,
                CreatedDate = DateTime.Now
            };
            _context.AiinteractionFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
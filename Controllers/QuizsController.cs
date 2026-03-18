using Afri.Models;
using Afri.Utilities;
using Afri.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Afri.Controllers
{
    public class QuizsController : Controller
    {
        private readonly AFRILEARNContext _context;
        private readonly CurrentUser _currentUser;

        public QuizsController(AFRILEARNContext context, CurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var aFRILEARNContext = _context.Quizzes.Include(q => q.Topic);
            return View(await aFRILEARNContext.ToListAsync());
        }

        /// <summary>Alias for Index (legacy links).</summary>
        public async Task<IActionResult> Indexx()
        {
            return await Index();
        }

        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var quiz = await _context.Quizzes
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null) return NotFound();

            return View(quiz);
        }

        // GET: Quizs/Create
        // Instructor flow: create quiz from a Topic (topicId) inside a Subject.
        public IActionResult Create(int? topicId)
        {
            if (!_currentUser.IsInstructor())
            {
                return Forbid();
            }

            var model = new QuizCreateViewModel();

            if (topicId.HasValue)
            {
                var topic = _context.Topics
                    .Include(t => t.Subject)
                    .FirstOrDefault(t => t.Id == topicId.Value);

                if (topic != null)
                {
                    model.TopicId = topicId;
                    model.TopicName = topic.Title;
                    model.SubjectName = topic.Subject?.Name;
                    model.SubjectId = topic.SubjectId;
                }
            }

            model.Topics = new SelectList(_context.Topics, "Id", "Title", topicId);

            return View(model);
        }

        // POST: Quizs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizCreateViewModel model)
        {
            if (!_currentUser.IsInstructor())
            {
                return Forbid();
            }

            // Require TopicId (from hidden field when coming from topic page, or from dropdown)
            if (!model.TopicId.HasValue || model.TopicId.Value <= 0)
            {
                ModelState.AddModelError("TopicId", "Please select a topic.");
            }
            else
            {
                // Ensure the topic exists
                var topicExists = await _context.Topics.AnyAsync(t => t.Id == model.TopicId.Value);
                if (!topicExists)
                {
                    ModelState.AddModelError("TopicId", "Selected topic is invalid.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var quiz = new Quiz
                    {
                        TopicId = model.TopicId.Value,
                        Title = model.Title?.Trim() ?? "Untitled Quiz",
                        TimeLimit = model.TimeLimit,
                        PassingScore = model.PassingScore,
                        IsPremium = model.IsPremium,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    _context.Add(quiz);
                    await _context.SaveChangesAsync();

                    // redirect into ManageQuestions so instructor can add questions immediately
                    return RedirectToAction(nameof(ManageQuestions), new { quizId = quiz.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Could not save quiz: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }

            // Repopulate view model so the form shows again with errors
            if (model.TopicId.HasValue)
            {
                var topic = await _context.Topics
                    .Include(t => t.Subject)
                    .FirstOrDefaultAsync(t => t.Id == model.TopicId.Value);
                if (topic != null)
                {
                    model.TopicName = topic.Title;
                    model.SubjectName = topic.Subject?.Name;
                    model.SubjectId = topic.SubjectId;
                }
            }

            model.Topics = new SelectList(await _context.Topics.ToListAsync(), "Id", "Title", model.TopicId);
            return View(model);
        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return NotFound();

            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Title", quiz.TopicId);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicId,Title,TimeLimit,PassingScore,IsPremium,CreatedDate,ModifiedDate")] Quiz quiz)
        {
            if (id != quiz.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Title", quiz.TopicId);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var quiz = await _context.Quizzes
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null) return NotFound();

            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }

        // Show take quiz page
        public async Task<IActionResult> TakeQuiz(int quizId)
        {
            // require login for taking quizzes
            int userId = _currentUser.GetUserId();
            if (userId == 0)
            {
                TempData["Error"] = "Please sign in to take quizzes.";
                return RedirectToAction("Signin", "Users");
            }

            var quiz = await _context.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.QuizOptions)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null) return NotFound();

            return View(quiz);
        }

        // POST: Submit Quiz
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuiz(int quizId)
        {
            int userId = _currentUser.GetUserId();
            if (userId == 0)
            {
                TempData["Error"] = "Please sign in to submit quizzes.";
                return RedirectToAction("Signin", "Users");
            }

            // Create the Attempt record
            var attempt = new StudentQuizAttempt
            {
                StudentId = userId,
                QuizId = quizId,
                AttemptDate = DateTime.Now,
                Score = 0,
                IsPassed = false
            };

            _context.StudentQuizAttempts.Add(attempt);
            await _context.SaveChangesAsync(); // generate attempt.Id

            int correctAnswers = 0;
            var questions = await _context.QuizQuestions
                .Include(q => q.QuizOptions)
                .Where(q => q.QuizId == quizId)
                .ToListAsync();

            foreach (var question in questions)
            {
                bool isCorrect = false;

                // read posted values defensively
                var form = Request.Form;

                if (question.QuestionType == "Short Answer" || question.QuestionType == "Fill in the Blank")
                {
                    string textAnswer = form[$"textAnswers[{question.Id}]"].ToString();
                    var correctOption = question.QuizOptions.FirstOrDefault(o => o.IsCorrect == true);

                    if (correctOption != null && !string.IsNullOrWhiteSpace(textAnswer) &&
                        textAnswer.Trim().Equals(correctOption.OptionText?.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        isCorrect = true;
                    }

                    _context.StudentQuizAnswers.Add(new StudentQuizAnswer
                    {
                        AttemptId = attempt.Id,
                        QuestionId = question.Id,
                        AnswerText = textAnswer,
                        SelectedOptionId = null
                    });
                }
                else if (question.QuestionType == "Multiple Answer")
                {
                    var selectedValues = form[$"selectedOptions[{question.Id}]"].ToArray().ToList();
                    var correctOptionIds = question.QuizOptions.Where(o => o.IsCorrect == true).Select(o => o.Id.ToString()).ToList();

                    if (selectedValues.Any())
                    {
                        if (selectedValues.Count == correctOptionIds.Count && !selectedValues.Except(correctOptionIds).Any())
                        {
                            isCorrect = true;
                        }

                        foreach (var val in selectedValues)
                        {
                            if (int.TryParse(val, out int sId))
                            {
                                _context.StudentQuizAnswers.Add(new StudentQuizAnswer
                                {
                                    AttemptId = attempt.Id,
                                    QuestionId = question.Id,
                                    SelectedOptionId = sId
                                });
                            }
                        }
                    }
                }
                else // Multiple Choice / TrueFalse (single)
                {
                    var selectedVal = form[$"singleOptions[{question.Id}]"].ToString();
                    if (!string.IsNullOrEmpty(selectedVal) && int.TryParse(selectedVal, out int selectedId))
                    {
                        isCorrect = question.QuizOptions.Any(o => o.Id == selectedId && o.IsCorrect == true);

                        _context.StudentQuizAnswers.Add(new StudentQuizAnswer
                        {
                            AttemptId = attempt.Id,
                            QuestionId = question.Id,
                            SelectedOptionId = selectedId
                        });
                    }
                }

                if (isCorrect) correctAnswers++;
            }

            // Calculate final score percentage
            decimal percentage = questions.Any() ? ((decimal)correctAnswers / questions.Count) * 100 : 0;

            // Fetch quiz to check passing requirements
            var quizInfo = await _context.Quizzes.FindAsync(quizId);

            attempt.Score = percentage;
            attempt.IsPassed = percentage >= (quizInfo?.PassingScore ?? 50);

            await _context.SaveChangesAsync();

            return RedirectToAction("QuizResult", new { id = attempt.Id });
        }

        // GET: Quizs/QuizResult/5
        public async Task<IActionResult> QuizResult(int id)
        {
            var result = await _context.StudentQuizAttempts
                .Include(a => a.Quiz)
                .Include(a => a.StudentQuizAnswers)
                .ThenInclude(sqa => sqa.SelectedOption)
                .Include(a => a.StudentQuizAnswers)
                .ThenInclude(sqa => sqa.Question)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (result == null) return NotFound();

            return View(result);
        }

        // GET: Quizs/ManageQuestions/5
        public async Task<IActionResult> ManageQuestions(int quizId)
        {
            // only instructors should manage questions
            if (!_currentUser.IsInstructor()) return Forbid();

            var quiz = await _context.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.QuizOptions)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null) return NotFound();

            return View(quiz);
        }

        // GET: Quizs/CreateQuestion
        public IActionResult CreateQuestion(int quizId)
        {
            if (quizId <= 0) return RedirectToAction("Index");

            if (!_currentUser.IsInstructor()) return Forbid();

            ViewBag.QuizId = quizId;
            return View();
        }

        // POST: Quizs/CreateQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(
            int quizId,
            string questionText,
            string questionType,
            string difficultyLevel,
            int marks,
            List<string> optionsText,
            List<int> correctOptionIndices,
            string submitAction)
        {
            if (!_currentUser.IsInstructor()) return Forbid();

            if (quizId <= 0) return BadRequest("Invalid Quiz");

            try
            {
                // Defensive binding: if optionsText or correctOptionIndices are null, read from Request.Form
                if ((optionsText == null || !optionsText.Any()) && Request.Form.TryGetValue("optionsText", out StringValues sv))
                {
                    optionsText = sv.ToList();
                }

                if ((correctOptionIndices == null || !correctOptionIndices.Any()) && Request.Form.TryGetValue("correctOptionIndices", out StringValues cv))
                {
                    correctOptionIndices = cv.Select(s =>
                    {
                        int v;
                        return int.TryParse(s, out v) ? v : -1;
                    }).Where(x => x >= 0).ToList();
                }

                // 1️⃣ Create Question
                var question = new QuizQuestion
                {
                    QuizId = quizId,
                    QuestionText = questionText,
                    QuestionType = questionType,
                    DifficultyLevel = difficultyLevel,
                    Marks = marks,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                _context.QuizQuestions.Add(question);
                await _context.SaveChangesAsync(); // generate question.Id

                // 2️⃣ Save Options
                if (questionType == "Short Answer" || questionType == "Fill in the Blank")
                {
                    if (optionsText != null && optionsText.Any())
                    {
                        var answer = optionsText.FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(answer))
                        {
                            _context.QuizOptions.Add(new QuizOption
                            {
                                QuestionId = question.Id,
                                OptionText = answer,
                                IsCorrect = true
                            });
                        }
                    }
                }
                else
                {
                    if (optionsText != null)
                    {
                        for (int i = 0; i < optionsText.Count; i++)
                        {
                            if (string.IsNullOrWhiteSpace(optionsText[i])) continue;

                            bool isCorrect = correctOptionIndices != null && correctOptionIndices.Contains(i);

                            _context.QuizOptions.Add(new QuizOption
                            {
                                QuestionId = question.Id,
                                OptionText = optionsText[i],
                                IsCorrect = isCorrect
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();

                if (submitAction == "SaveAndAddAnother")
                {
                    return RedirectToAction("CreateQuestion", new { quizId = quizId });
                }

                return RedirectToAction("ManageQuestions", new { quizId = quizId });
            }
            catch (Exception ex)
            {
                return Content("Error: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int questionId, int quizId)
        {
            var question = await _context.QuizQuestions
                .Include(q => q.QuizOptions)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question != null)
            {
                _context.QuizOptions.RemoveRange(question.QuizOptions);
                _context.QuizQuestions.Remove(question);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ManageQuestions", new { quizId = quizId });
        }
    }
}
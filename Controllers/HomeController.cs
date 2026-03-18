using System.Diagnostics;
using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afri.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AFRILEARNContext _context;
        private readonly CurrentUser _currentUser;

        public HomeController(ILogger<HomeController> logger, AFRILEARNContext context, CurrentUser currentUser)
        {
            _logger = logger;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> Index()
        {
            int userId = _currentUser.GetUserId();
            if (userId == 0)
                return View();

            var model = await BuildDashboardAsync(userId);
            return View(model);
        }

        private async Task<HomeDashboardViewModel> BuildDashboardAsync(int userId)
        {
            var vm = new HomeDashboardViewModel
            {
                UserName = _currentUser.GetUserName(),
                IsStudent = _currentUser.IsStudent()
            };

            if (vm.IsStudent)
            {
                vm.EnrolledSubjectsCount = await _context.StudentLearningPaths.CountAsync(s => s.StudentId == userId);
                vm.CompletedQuizzesCount = await _context.StudentQuizAttempts.CountAsync(q => q.StudentId == userId);
                vm.LessonsCompletedCount = await _context.StudentProgresses.CountAsync(p => p.StudentId == userId && (p.CompletionStatus == "Completed" || p.Score != null));

                var myScore = await _context.LeaderboardScores.FirstOrDefaultAsync(s => s.StudentId == userId);
                vm.MyLeaderboardPoints = myScore?.TotalPoints;
                var ranked = await _context.LeaderboardScores
                    .Include(s => s.Student)
                    .OrderByDescending(s => s.TotalPoints)
                    .Take(10)
                    .ToListAsync();
                int rank = 1;
                foreach (var s in ranked)
                {
                    vm.LeaderboardTop.Add(new LeaderboardEntryDto
                    {
                        Rank = rank++,
                        StudentName = s.Student?.FullName ?? "Student",
                        Points = s.TotalPoints,
                        IsCurrentUser = s.StudentId == userId
                    });
                    if (s.StudentId == userId) vm.MyLeaderboardRank = s.Rank ?? rank - 1;
                }
                if (vm.MyLeaderboardRank == 0 && myScore != null) vm.MyLeaderboardRank = myScore.Rank ?? 0;

                var achievements = await _context.StudentAchievements
                    .Include(sa => sa.Achievement)
                    .Where(sa => sa.StudentId == userId)
                    .OrderByDescending(sa => sa.AchievedDate)
                    .Take(10)
                    .Select(sa => new StudentAchievementDto
                    {
                        Name = sa.Achievement.Name,
                        Description = sa.Achievement.Description,
                        Points = sa.Achievement.Points,
                        EarnedDate = sa.AchievedDate
                    })
                    .ToListAsync();
                vm.Achievements = achievements;

                var progress = await _context.StudentProgresses
                    .Include(p => p.Lesson).ThenInclude(l => l!.Topic).ThenInclude(t => t!.Subject)
                    .Where(p => p.StudentId == userId)
                    .OrderByDescending(p => p.LastUpdated)
                    .Take(5)
                    .Select(p => new StudentProgressDto
                    {
                        LessonTitle = p.Lesson.Title,
                        SubjectName = p.Lesson.Topic != null ? p.Lesson.Topic.Subject!.Name : null,
                        LastUpdated = p.LastUpdated,
                        Score = p.Score
                    })
                    .ToListAsync();
                vm.RecentProgress = progress;

                var lessons = await _context.Lessons
                    .Include(l => l.Topic).ThenInclude(t => t!.Subject).ThenInclude(s => s!.GradeLevel)
                    .Where(l => l.Topic != null)
                    .OrderBy(l => l.CreatedDate)
                    .Take(5)
                    .Select(l => new RecommendedLessonDto
                    {
                        LessonId = l.Id,
                        LessonTitle = l.Title,
                        SubjectName = l.Topic!.Subject!.Name,
                        TopicTitle = l.Topic.Title,
                        GradeName = l.Topic.Subject.GradeLevel != null ? l.Topic.Subject.GradeLevel.Name : null,
                        SubjectId = l.Topic.SubjectId
                    })
                    .ToListAsync();
                vm.RecommendedLessons = lessons;
            }
            else
            {
                vm.TotalLessons = await _context.Lessons.CountAsync();
                vm.TotalQuizQuestions = await _context.QuizQuestions.CountAsync();
                var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name != null && r.Name.ToLower() == "student");
                vm.ActiveStudentsCount = studentRole != null
                    ? await _context.Users.CountAsync(u => u.RoleId == studentRole.Id)
                    : 0;
                vm.AIInteractionsCount = await _context.StudentQuestions.CountAsync();

                var recentAttempts = await _context.StudentQuizAttempts
                    .Include(a => a.Student)
                    .Include(a => a.Quiz)
                    .OrderByDescending(a => a.AttemptDate)
                    .Take(5)
                    .ToListAsync();
                foreach (var a in recentAttempts)
                {
                    vm.InstructorRecentActivity.Add(new RecentActivityDto
                    {
                        TimeAgo = a.AttemptDate.HasValue ? ToTimeAgo(a.AttemptDate.Value) : "",
                        Title = $"{a.Student?.FullName ?? "Student"} completed {a.Quiz?.Title ?? "Quiz"}",
                        Detail = a.Score.HasValue ? $"Score: {a.Score.Value:0}%" : ""
                    });
                }
                var recentQuestions = await _context.StudentQuestions
                    .Include(q => q.Student)
                    .OrderByDescending(q => q.CreatedDate)
                   // .OrderByDescending(q => q.AskedAt)
                    .Take(3)
                    .ToListAsync();
                foreach (var q in recentQuestions)
                {
                    vm.InstructorRecentActivity.Add(new RecentActivityDto
                    {
                        TimeAgo = q.CreatedDate.HasValue ? ToTimeAgo(q.CreatedDate.Value) : "",
                        Title = $"{q.Student?.FullName ?? "Student"} asked AI",
                        Detail = q.QuestionText != null && q.QuestionText.Length > 50 ? q.QuestionText[..50] + "…" : q.QuestionText ?? ""
                    });
                }
                vm.InstructorRecentActivity = vm.InstructorRecentActivity.Take(5).ToList();
            }

            return vm;
        }

        private static string ToTimeAgo(DateTime dt)
        {
            var span = DateTime.Now - dt;
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} min ago";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours} hr ago";
            if (span.TotalDays < 7) return $"{(int)span.TotalDays} day(s) ago";
            return dt.ToString("MMM d");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

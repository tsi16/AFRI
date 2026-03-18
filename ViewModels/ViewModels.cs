using System.ComponentModel.DataAnnotations;

namespace Afri.Models
{
    // User authentication models
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grade level is required")]
        public int GradeLevel { get; set; }

        [Required(ErrorMessage = "Language preference is required")]
        public string Language { get; set; } = "en";
    }

    // Dashboard model
    public class DashboardViewModel
    {
        public string StudentName { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
        public int CurrentStreak { get; set; }
        public int LessonsCompleted { get; set; }
        public int QuizzesTaken { get; set; }
        public double OverallProgress { get; set; }
        public List<RecentActivity> RecentActivities { get; set; } = new();
        public List<QuickAction> QuickActions { get; set; } = new();
    }

    public class RecentActivity
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
    }

    public class QuickAction
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    /// <summary>Real dashboard data for Home (student + instructor).</summary>
    public class HomeDashboardViewModel
    {
        public bool IsStudent { get; set; }
        public string UserName { get; set; } = string.Empty;

        // Student
        public int EnrolledSubjectsCount { get; set; }
        public int CompletedQuizzesCount { get; set; }
        public int CurrentStreakDays { get; set; }
        public int LessonsCompletedCount { get; set; }
        public int MyLeaderboardRank { get; set; }
        public int? MyLeaderboardPoints { get; set; }
        public List<LeaderboardEntryDto> LeaderboardTop { get; set; } = new();
        public List<RecommendedLessonDto> RecommendedLessons { get; set; } = new();
        public List<StudentAchievementDto> Achievements { get; set; } = new();
        public List<StudentProgressDto> RecentProgress { get; set; } = new();

        // Instructor
        public int TotalLessons { get; set; }
        public int TotalQuizQuestions { get; set; }
        public int ActiveStudentsCount { get; set; }
        public int AIInteractionsCount { get; set; }
        public List<RecentActivityDto> InstructorRecentActivity { get; set; } = new();
    }

    public class LeaderboardEntryDto
    {
        public int Rank { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int? Points { get; set; }
        public bool IsCurrentUser { get; set; }
    }

    public class RecommendedLessonDto
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string TopicTitle { get; set; } = string.Empty;
        public string? GradeName { get; set; }
        public int SubjectId { get; set; }
    }

    public class StudentAchievementDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Points { get; set; }
        public DateTime? EarnedDate { get; set; }
    }

    public class StudentProgressDto
    {
        public string LessonTitle { get; set; } = string.Empty;
        public string? SubjectName { get; set; }
        public DateTime? LastUpdated { get; set; }
        public decimal? Score { get; set; }
    }

    public class RecentActivityDto
    {
        public string TimeAgo { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }

    // Subject model
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GradeLabel { get; set; } = string.Empty;
        public string GradeRange { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string IconColor { get; set; } = string.Empty;
        public string BgColor { get; set; } = string.Empty;
        public int TopicCount { get; set; }
        public int LessonCount { get; set; }
        public int Progress { get; set; }
    }

    // Lesson model
    public class LessonViewModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public List<TopicViewModel> Topics { get; set; } = new();
    }

    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LessonCount { get; set; }
        public int CompletedLessons { get; set; }
        public int Progress { get; set; }
        public bool IsLocked { get; set; }
        public List<LessonDownloadInfo> Lessons { get; set; } = new();
    }

    // Lesson download info for offline content
    public class LessonDownloadInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDownloaded { get; set; }
        public string? DownloadedFilePath { get; set; }
        public DateTime? DownloadedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public List<LanguageTranslation> AvailableTranslations { get; set; } = new();
    }

    public class LanguageTranslation
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        public string? TranslatedContent { get; set; }
        public string? VoiceExplanationUrl { get; set; }
        public bool IsHumanValidated { get; set; }
    }

    // AI Tutor model
    public class AiTutorViewModel
    {
        public List<ChatMessage> Messages { get; set; } = new();
        public string CurrentMessage { get; set; } = string.Empty;
        public string SelectedLanguage { get; set; } = "en";
    }

    public class ChatMessage
    {
        public bool IsUser { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
    }

    // Quiz model
    public class QuizViewModel
    {
        public List<QuizCategory> Categories { get; set; } = new();
    }

    public class QuizCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int QuizCount { get; set; }
        public int QuestionsCount { get; set; }
        public string Difficulty { get; set; } = string.Empty;
    }

    // Progress model
    public class ProgressViewModel
    {
        public WeeklyProgress WeeklyProgress { get; set; } = new();
        public List<SubjectProgress> SubjectProgresses { get; set; } = new();
        public List<Achievement> Achievements { get; set; } = new();
    }

    public class WeeklyProgress
    {
        public int[] DailyMinutes { get; set; } = new int[7];
        public int TotalMinutes { get; set; }
        public int LessonsCompleted { get; set; }
        public int QuizzesTaken { get; set; }
    }

    public class SubjectProgress
    {
        public string SubjectName { get; set; } = string.Empty;
        public string IconColor { get; set; } = string.Empty;
        public int Progress { get; set; }
        public int LessonsCompleted { get; set; }
        public int TotalLessons { get; set; }
    }

    public partial class Achievement
    {
       // public string Name { get; set; } = string.Empty;
      //  public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string UnlockedDate { get; set; } = string.Empty;
        public bool IsUnlocked { get; set; }
    }

    // Leaderboard model
    public class LeaderboardViewModel
    {
        public List<LeaderboardEntry> TopStudents { get; set; } = new();
        public string CurrentUserRank { get; set; } = string.Empty;
    }

    public class LeaderboardEntry
    {
        public int Rank { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Streak { get; set; }
        public string Badge { get; set; } = string.Empty;
    }

    // Download model
    public class DownloadViewModel
    {
        public List<DownloadedLesson> DownloadedLessons { get; set; } = new();
        public long TotalStorageUsed { get; set; }
        public long TotalStorageAvailable { get; set; }
    }

    public partial class DownloadedLesson
    {
       // public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string TopicName { get; set; } = string.Empty;
        public string LessonName { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public string DownloadedDate { get; set; } = string.Empty;
        public bool IsDownloaded { get; set; }
    }

    // Admin model
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalSubscriptions { get; set; }
        public int PendingReviews { get; set; }
        public List<RecentUser> RecentUsers { get; set; } = new();
        public List<PendingContent> PendingContents { get; set; } = new();
    }

    public class RecentUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string JoinedDate { get; set; } = string.Empty;
    }

    public class PendingContent
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SubmittedBy { get; set; } = string.Empty;
        public string SubmittedDate { get; set; } = string.Empty;
    }
}
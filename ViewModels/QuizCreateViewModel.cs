using Microsoft.AspNetCore.Mvc.Rendering;

namespace Afri.ViewModels
{
    public class QuizCreateViewModel
    {
        public int? TopicId { get; set; }

        public string Title { get; set; }

        public int? TimeLimit { get; set; }

        public int PassingScore { get; set; } = 50;

        public bool IsPremium { get; set; }

        public string? TopicName { get; set; }

        public string? SubjectName { get; set; }

        public int? SubjectId { get; set; }

        public SelectList? Topics { get; set; }
    }
}
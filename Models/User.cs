using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Index("Email", Name = "UQ__Users__A9D1053403A62C3C", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string FullName { get; set; } = null!;

    [StringLength(10)]
    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? PhoneNumber { get; set; }

    public string? PasswordHash { get; set; }

    public int RoleId { get; set; }

    public int? InstitutionId { get; set; }

    public int? PreferredLanguageId { get; set; }

    public int? GradeLevelId { get; set; }

    [StringLength(500)]
    public string? ProfilePhotoUrl { get; set; }

    [StringLength(50)]
    public string? AccountStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public int? ParentId { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("Children")]
    public virtual User? Parent { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<User> Children { get; set; } = new List<User>();

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("AssignedTeacher")]
    public virtual ICollection<ContentReviewAssignment> ContentReviewAssignments { get; set; } = new List<ContentReviewAssignment>();

    [InverseProperty("Student")]
    public virtual ICollection<DownloadedLesson> DownloadedLessons { get; set; } = new List<DownloadedLesson>();

    [ForeignKey("GradeLevelId")]
    [InverseProperty("Users")]
    public virtual GradeLevel? GradeLevel { get; set; }

    [ForeignKey("InstitutionId")]
    [InverseProperty("Users")]
    public virtual Institution? Institution { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<LeaderboardScore> LeaderboardScores { get; set; } = new List<LeaderboardScore>();

    [InverseProperty("Student")]
    public virtual ICollection<OfflineSyncLog> OfflineSyncLogs { get; set; } = new List<OfflineSyncLog>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("PreferredLanguageId")]
    [InverseProperty("Users")]
    public virtual Language? PreferredLanguage { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<StudentAchievement> StudentAchievements { get; set; } = new List<StudentAchievement>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentActivityLog> StudentActivityLogs { get; set; } = new List<StudentActivityLog>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentLearningPath> StudentLearningPaths { get; set; } = new List<StudentLearningPath>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentProgress> StudentProgresses { get; set; } = new List<StudentProgress>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; } = new List<StudentQuestion>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentQuizAttempt> StudentQuizAttempts { get; set; } = new List<StudentQuizAttempt>();

    [InverseProperty("UploadedByUser")]
    public virtual ICollection<TopicMaterial> TopicMaterials { get; set; } = new List<TopicMaterial>();

    [InverseProperty("ReviewerUser")]
    public virtual ICollection<TranslationReviewLog> TranslationReviewLogs { get; set; } = new List<TranslationReviewLog>();

    [InverseProperty("User")]
    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}

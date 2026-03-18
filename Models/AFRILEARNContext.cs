using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class AFRILEARNContext : DbContext
{
    public AFRILEARNContext()
    {
    }

    public AFRILEARNContext(DbContextOptions<AFRILEARNContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<AiinteractionFeedback> AiinteractionFeedbacks { get; set; }

    public virtual DbSet<Airesponse> Airesponses { get; set; }

    public virtual DbSet<ContentReviewAssignment> ContentReviewAssignments { get; set; }

    public virtual DbSet<CurriculumStandard> CurriculumStandards { get; set; }

    public virtual DbSet<DownloadedLesson> DownloadedLessons { get; set; }

    public virtual DbSet<GradeLevel> GradeLevels { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<InstitutionSubscription> InstitutionSubscriptions { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LeaderboardScore> LeaderboardScores { get; set; }

    public virtual DbSet<LearningPath> LearningPaths { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonTranslation> LessonTranslations { get; set; }

    public virtual DbSet<OfflineSyncLog> OfflineSyncLogs { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizOption> QuizOptions { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<StudentAchievement> StudentAchievements { get; set; }

    public virtual DbSet<StudentActivityLog> StudentActivityLogs { get; set; }

    public virtual DbSet<StudentLearningPath> StudentLearningPaths { get; set; }

    public virtual DbSet<StudentProgress> StudentProgresses { get; set; }

    public virtual DbSet<StudentQuestion> StudentQuestions { get; set; }

    public virtual DbSet<StudentQuizAnswer> StudentQuizAnswers { get; set; }

    public virtual DbSet<StudentQuizAttempt> StudentQuizAttempts { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<SystemUsageStatistic> SystemUsageStatistics { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<TopicMaterial> TopicMaterials { get; set; }

    public virtual DbSet<TranslationReviewLog> TranslationReviewLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=AFRILEARN;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3214EC0792DE51DD");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Points).HasDefaultValue(0);
        });

        modelBuilder.Entity<AiinteractionFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AIIntera__3214EC07E8C21032");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Airesponse).WithMany(p => p.AiinteractionFeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AIInterac__AIRes__01142BA1");
        });

        modelBuilder.Entity<Airesponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AIRespon__3214EC075CB0CC8C");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsValidated).HasDefaultValue(false);

            entity.HasOne(d => d.StudentQuestion).WithMany(p => p.Airesponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AIRespons__Stude__7C4F7684");
        });

        modelBuilder.Entity<ContentReviewAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContentR__3214EC078E549168");

            entity.Property(e => e.AssignedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedTeacher).WithMany(p => p.ContentReviewAssignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContentRe__Assig__5224328E");

            entity.HasOne(d => d.Lesson).WithMany(p => p.ContentReviewAssignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContentRe__Lesso__531856C7");
        });

        modelBuilder.Entity<CurriculumStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC07C9395112");

            entity.HasOne(d => d.GradeLevel).WithMany(p => p.CurriculumStandards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curriculu__Grade__57DD0BE4");

            entity.HasOne(d => d.Subject).WithMany(p => p.CurriculumStandards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curriculu__Subje__56E8E7AB");
        });

        modelBuilder.Entity<DownloadedLesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Download__3214EC076D54B3C8");

            entity.Property(e => e.DownloadDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsSynced).HasDefaultValue(false);

            entity.HasOne(d => d.Lesson).WithMany(p => p.DownloadedLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Downloade__Lesso__282DF8C2");

            entity.HasOne(d => d.Student).WithMany(p => p.DownloadedLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Downloade__Stude__2739D489");
        });

        modelBuilder.Entity<GradeLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GradeLev__3214EC073F6D66D0");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Institut__3214EC076BF725BE");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<InstitutionSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Institut__3214EC07F5814302");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Institution).WithMany(p => p.InstitutionSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Instituti__Insti__47A6A41B");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.InstitutionSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Instituti__Subsc__489AC854");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC07E3F79A84");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRtl).HasDefaultValue(false);
        });

        modelBuilder.Entity<LeaderboardScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leaderbo__3214EC0711209BF2");

            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rank).HasDefaultValue(0);
            entity.Property(e => e.TotalPoints).HasDefaultValue(0);

            entity.HasOne(d => d.Student).WithMany(p => p.LeaderboardScores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leaderboa__Stude__395884C4");
        });

        modelBuilder.Entity<LearningPath>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Learning__3214EC07849A2B03");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lessons__3214EC0714BFC895");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDownloadable).HasDefaultValue(false);
            entity.Property(e => e.IsPremium).HasDefaultValue(false);
            entity.Property(e => e.IsPublished).HasDefaultValue(false);

            entity.HasOne(d => d.Topic).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lessons__TopicId__628FA481");
        });

        modelBuilder.Entity<LessonTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LessonTr__3214EC07ED10D5C7");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsHumanValidated).HasDefaultValue(false);

            entity.HasOne(d => d.Language).WithMany(p => p.LessonTranslations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LessonTra__Langu__6A30C649");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonTranslations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LessonTra__Lesso__693CA210");
        });

        modelBuilder.Entity<OfflineSyncLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OfflineS__3214EC079A7BB571");

            entity.Property(e => e.SyncDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Student).WithMany(p => p.OfflineSyncLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfflineSy__Stude__2CF2ADDF");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07CDD3B493");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Institution).WithMany(p => p.Payments).HasConstraintName("FK__Payments__Instit__4E53A1AA");

            entity.HasOne(d => d.User).WithMany(p => p.Payments).HasConstraintName("FK__Payments__UserId__4D5F7D71");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07C92948AA");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quizzes__3214EC07AC123607");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsPremium).HasDefaultValue(false);

            entity.HasOne(d => d.Topic).WithMany(p => p.Quizzes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__TopicId__04E4BC85");
        });

        modelBuilder.Entity<QuizOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuizOpti__3214EC07549170BA");

            entity.Property(e => e.IsCorrect).HasDefaultValue(false);

            entity.HasOne(d => d.Question).WithMany(p => p.QuizOptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizOptio__Quest__0D7A0286");
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuizQues__3214EC0715DB4996");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizQuestions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizQuest__QuizI__09A971A2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07863B324A");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC07D367D4D0");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolePermi__Permi__403A8C7D");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolePermi__RoleI__3F466844");
        });

        modelBuilder.Entity<StudentAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentA__3214EC075351ED77");

            entity.Property(e => e.AchievedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Achievement).WithMany(p => p.StudentAchievements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Achie__3587F3E0");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAchievements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Stude__3493CFA7");
        });

        modelBuilder.Entity<StudentActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentA__3214EC07D5C0AEF4");

            entity.Property(e => e.ActivityDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentActivityLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Stude__5AB9788F");
        });

        modelBuilder.Entity<StudentLearningPath>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentL__3214EC073D7898AE");

            entity.Property(e => e.ProgressPercent).HasDefaultValue(0m);

            entity.HasOne(d => d.LearningPath).WithMany(p => p.StudentLearningPaths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentLe__Learn__1EA48E88");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentLearningPaths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentLe__Stude__1DB06A4F");
        });

        modelBuilder.Entity<StudentProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentP__3214EC0723B5BCB1");

            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.StudentProgresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentPr__Lesso__236943A5");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentProgresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentPr__Stude__22751F6C");
        });

        modelBuilder.Entity<StudentQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentQ__3214EC07A0C070A3");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsVoiceInput).HasDefaultValue(false);

            entity.HasOne(d => d.Language).WithMany(p => p.StudentQuestions).HasConstraintName("FK__StudentQu__Langu__778AC167");

            entity.HasOne(d => d.Lesson).WithMany(p => p.StudentQuestions).HasConstraintName("FK__StudentQu__Lesso__76969D2E");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentQuestions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Stude__73BA3083");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentQuestions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Subje__74AE54BC");

            entity.HasOne(d => d.Topic).WithMany(p => p.StudentQuestions).HasConstraintName("FK__StudentQu__Topic__75A278F5");
        });

        modelBuilder.Entity<StudentQuizAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentQ__3214EC07F4BD33F0");

            entity.HasOne(d => d.Attempt).WithMany(p => p.StudentQuizAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Attem__160F4887");

            entity.HasOne(d => d.Question).WithMany(p => p.StudentQuizAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Quest__17036CC0");

            entity.HasOne(d => d.SelectedOption).WithMany(p => p.StudentQuizAnswers).HasConstraintName("FK__StudentQu__Selec__17F790F9");
        });

        modelBuilder.Entity<StudentQuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentQ__3214EC07AE3E42CC");

            entity.Property(e => e.AttemptDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Quiz).WithMany(p => p.StudentQuizAttempts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__QuizI__123EB7A3");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentQuizAttempts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Stude__114A936A");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC078A802916");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsCore).HasDefaultValue(true);

            entity.HasOne(d => d.GradeLevel).WithMany(p => p.Subjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__GradeL__59FA5E80");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07FDEDA5D9");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<SystemUsageStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC07E0843A5A");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3214EC0785E74B05");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Subject).WithMany(p => p.Topics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Topics__SubjectI__5EBF139D");
        });

        modelBuilder.Entity<TopicMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TopicMat__3214EC07");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ResourceType).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Topic).WithMany(p => p.TopicMaterials)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.TopicId);

            entity.HasOne(d => d.UploadedByUser).WithMany(p => p.TopicMaterials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(d => d.UploadedByUserId);
        });

        modelBuilder.Entity<TranslationReviewLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Translat__3214EC077AF74952");

            entity.Property(e => e.ReviewedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LessonTranslation).WithMany(p => p.TranslationReviewLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Translati__Lesso__6EF57B66");

            entity.HasOne(d => d.ReviewerUser).WithMany(p => p.TranslationReviewLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Translati__Revie__6FE99F9F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0715A23734");

            entity.Property(e => e.AccountStatus).HasDefaultValue("Active");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.GradeLevel).WithMany(p => p.Users).HasConstraintName("FK__Users__GradeLeve__5441852A");

            entity.HasOne(d => d.Institution).WithMany(p => p.Users).HasConstraintName("FK__Users__Instituti__52593CB8");

            entity.HasOne(d => d.PreferredLanguage).WithMany(p => p.Users).HasConstraintName("FK__Users__Preferred__534D60F1");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__5165187F");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubs__3214EC07659FDF66");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.UserSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Subsc__42E1EEFE");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__UserI__41EDCAC5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

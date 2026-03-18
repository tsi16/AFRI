using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afri.Migrations
{
    /// <inheritdoc />
    public partial class AddParentToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlockedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUnlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Achievem__3214EC0792DE51DD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GradeLev__3214EC073F6D66D0", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Institut__3214EC076BF725BE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsRTL = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Language__3214EC07E3F79A84", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__3214EC07849A2B03", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permissi__3214EC07C92948AA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC07863B324A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    FeatureFlags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__3214EC07FDEDA5D9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsageStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatisticDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActiveUsers = table.Column<int>(type: "int", nullable: true),
                    LessonsAccessed = table.Column<int>(type: "int", nullable: true),
                    QuizzesAttempted = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SystemUs__3214EC07E0843A5A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GradeLevelId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCore = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subjects__3214EC078A802916", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Subjects__GradeL__59FA5E80",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RolePerm__3214EC07D367D4D0", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RolePermi__Permi__403A8C7D",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__RolePermi__RoleI__3F466844",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: true),
                    PreferredLanguageId = table.Column<int>(type: "int", nullable: true),
                    GradeLevelId = table.Column<int>(type: "int", nullable: true),
                    ProfilePhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Active"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC0715A23734", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Users__GradeLeve__5441852A",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Users__Instituti__52593CB8",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Users__Preferred__534D60F1",
                        column: x => x.PreferredLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Users__RoleId__5165187F",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstitutionSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Institut__3214EC07F5814302", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Instituti__Insti__47A6A41B",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Instituti__Subsc__489AC854",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CurriculumStandards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GradeLevelId = table.Column<int>(type: "int", nullable: false),
                    StandardCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__3214EC07C9395112", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Curriculu__Grade__57DD0BE4",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Curriculu__Subje__56E8E7AB",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    EstimatedHours = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Topics__3214EC0785E74B05", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Topics__SubjectI__5EBF139D",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Rank = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leaderbo__3214EC0711209BF2", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Leaderboa__Stude__395884C4",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfflineSyncLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SyncDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DeviceInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfflineS__3214EC079A7BB571", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OfflineSy__Stude__2CF2ADDF",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    InstitutionId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__3214EC07CDD3B493", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Payments__Instit__4E53A1AA",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Payments__UserId__4D5F7D71",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    AchievedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentA__3214EC075351ED77", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentAc__Achie__3587F3E0",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentAc__Stude__3493CFA7",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceId = table.Column<int>(type: "int", nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentA__3214EC07D5C0AEF4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentAc__Stude__5AB9788F",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentLearningPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LearningPathId = table.Column<int>(type: "int", nullable: false),
                    ProgressPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 0m),
                    LastAccessed = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentL__3214EC073D7898AE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentLe__Learn__1EA48E88",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentLe__Stude__1DB06A4F",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserSubs__3214EC07659FDF66", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserSubsc__Subsc__42E1EEFE",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserSubsc__UserI__41EDCAC5",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPremium = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsDownloadable = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lessons__3214EC0714BFC895", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Lessons__TopicId__628FA481",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: true),
                    PassingScore = table.Column<int>(type: "int", nullable: true),
                    IsPremium = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quizzes__3214EC07AC123607", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Quizzes__TopicId__04E4BC85",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopicMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExternalUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    UploadedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TopicMat__3214EC07", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicMaterials_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicMaterials_Users_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContentReviewAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedTeacherId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ReviewedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContentR__3214EC078E549168", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ContentRe__Assig__5224328E",
                        column: x => x.AssignedTeacherId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ContentRe__Lesso__531856C7",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DownloadedLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    DownloadDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSynced = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDownloaded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Download__3214EC076D54B3C8", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Downloade__Lesso__282DF8C2",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Downloade__Stude__2739D489",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    TranslatedContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoiceExplanationUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsHumanValidated = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LessonTr__3214EC07ED10D5C7", x => x.Id);
                    table.ForeignKey(
                        name: "FK__LessonTra__Langu__6A30C649",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__LessonTra__Lesso__693CA210",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TimeSpentMinutes = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentP__3214EC0723B5BCB1", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentPr__Lesso__236943A5",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentPr__Stude__22751F6C",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVoiceInput = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentQ__3214EC07A0C070A3", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentQu__Langu__778AC167",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Lesso__76969D2E",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Stude__73BA3083",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Subje__74AE54BC",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Topic__75A278F5",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizQues__3214EC0715DB4996", x => x.Id);
                    table.ForeignKey(
                        name: "FK__QuizQuest__QuizI__09A971A2",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsPassed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentQ__3214EC07AE3E42CC", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentQu__QuizI__123EB7A3",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Stude__114A936A",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TranslationReviewLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonTranslationId = table.Column<int>(type: "int", nullable: false),
                    ReviewerUserId = table.Column<int>(type: "int", nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Translat__3214EC077AF74952", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Translati__Lesso__6EF57B66",
                        column: x => x.LessonTranslationId,
                        principalTable: "LessonTranslations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Translati__Revie__6FE99F9F",
                        column: x => x.ReviewerUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AIResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsValidated = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AIRespon__3214EC075CB0CC8C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AIRespons__Stude__7C4F7684",
                        column: x => x.StudentQuestionId,
                        principalTable: "StudentQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizOpti__3214EC07549170BA", x => x.Id);
                    table.ForeignKey(
                        name: "FK__QuizOptio__Quest__0D7A0286",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AIInteractionFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AIResponseId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AIIntera__3214EC07E8C21032", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AIInterac__AIRes__01142BA1",
                        column: x => x.AIResponseId,
                        principalTable: "AIResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "int", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentQ__3214EC07F4BD33F0", x => x.Id);
                    table.ForeignKey(
                        name: "FK__StudentQu__Attem__160F4887",
                        column: x => x.AttemptId,
                        principalTable: "StudentQuizAttempts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Quest__17036CC0",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__StudentQu__Selec__17F790F9",
                        column: x => x.SelectedOptionId,
                        principalTable: "QuizOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIInteractionFeedback_AIResponseId",
                table: "AIInteractionFeedback",
                column: "AIResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_AIResponses_StudentQuestionId",
                table: "AIResponses",
                column: "StudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentReviewAssignments_AssignedTeacherId",
                table: "ContentReviewAssignments",
                column: "AssignedTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentReviewAssignments_LessonId",
                table: "ContentReviewAssignments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumStandards_GradeLevelId",
                table: "CurriculumStandards",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumStandards_SubjectId",
                table: "CurriculumStandards",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadedLessons_LessonId",
                table: "DownloadedLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadedLessons_StudentId",
                table: "DownloadedLessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionSubscriptions_InstitutionId",
                table: "InstitutionSubscriptions",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionSubscriptions_SubscriptionPlanId",
                table: "InstitutionSubscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "UQ__Language__A25C5AA7A145BE30",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardScores_StudentId",
                table: "LeaderboardScores",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TopicId",
                table: "Lessons",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTranslations_LanguageId",
                table: "LessonTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTranslations_LessonId",
                table: "LessonTranslations",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineSyncLogs_StudentId",
                table: "OfflineSyncLogs",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InstitutionId",
                table: "Payments",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizOptions_QuestionId",
                table: "QuizOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_TopicId",
                table: "Quizzes",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_AchievementId",
                table: "StudentAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_StudentId",
                table: "StudentAchievements",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivityLogs_StudentId",
                table: "StudentActivityLogs",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLearningPaths_LearningPathId",
                table: "StudentLearningPaths",
                column: "LearningPathId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLearningPaths_StudentId",
                table: "StudentLearningPaths",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgress_LessonId",
                table: "StudentProgress",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgress_StudentId",
                table: "StudentProgress",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestions_LanguageId",
                table: "StudentQuestions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestions_LessonId",
                table: "StudentQuestions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestions_StudentId",
                table: "StudentQuestions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestions_SubjectId",
                table: "StudentQuestions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestions_TopicId",
                table: "StudentQuestions",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_AttemptId",
                table: "StudentQuizAnswers",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_QuestionId",
                table: "StudentQuizAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_SelectedOptionId",
                table: "StudentQuizAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAttempts_QuizId",
                table: "StudentQuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAttempts_StudentId",
                table: "StudentQuizAttempts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GradeLevelId",
                table: "Subjects",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "UQ__Subjects__A25C5AA7488CB98D",
                table: "Subjects",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TopicMaterials_TopicId",
                table: "TopicMaterials",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicMaterials_UploadedByUserId",
                table: "TopicMaterials",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_SubjectId",
                table: "Topics",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationReviewLogs_LessonTranslationId",
                table: "TranslationReviewLogs",
                column: "LessonTranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationReviewLogs_ReviewerUserId",
                table: "TranslationReviewLogs",
                column: "ReviewerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GradeLevelId",
                table: "Users",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InstitutionId",
                table: "Users",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParentId",
                table: "Users",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferredLanguageId",
                table: "Users",
                column: "PreferredLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053403A62C3C",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionPlanId",
                table: "UserSubscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIInteractionFeedback");

            migrationBuilder.DropTable(
                name: "ContentReviewAssignments");

            migrationBuilder.DropTable(
                name: "CurriculumStandards");

            migrationBuilder.DropTable(
                name: "DownloadedLessons");

            migrationBuilder.DropTable(
                name: "InstitutionSubscriptions");

            migrationBuilder.DropTable(
                name: "LeaderboardScores");

            migrationBuilder.DropTable(
                name: "OfflineSyncLogs");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StudentAchievements");

            migrationBuilder.DropTable(
                name: "StudentActivityLogs");

            migrationBuilder.DropTable(
                name: "StudentLearningPaths");

            migrationBuilder.DropTable(
                name: "StudentProgress");

            migrationBuilder.DropTable(
                name: "StudentQuizAnswers");

            migrationBuilder.DropTable(
                name: "SystemUsageStatistics");

            migrationBuilder.DropTable(
                name: "TopicMaterials");

            migrationBuilder.DropTable(
                name: "TranslationReviewLogs");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "AIResponses");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "LearningPaths");

            migrationBuilder.DropTable(
                name: "StudentQuizAttempts");

            migrationBuilder.DropTable(
                name: "QuizOptions");

            migrationBuilder.DropTable(
                name: "LessonTranslations");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "StudentQuestions");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "GradeLevels");
        }
    }
}

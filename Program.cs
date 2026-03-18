using Microsoft.EntityFrameworkCore;
using Afri.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MVC Services
builder.Services.AddControllersWithViews();

// 2. Database Connection
var connectionString = builder.Configuration.GetConnectionString("AFRILEARNConnection");
builder.Services.AddDbContext<AFRILEARNContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Session Configuration (Essential for Authentication)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // User stays logged in for 1 hour
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Accessor for Session in Views (Optional but very helpful for Layouts)
builder.Services.AddHttpContextAccessor();
//builder.Services.AddHttpContextAccessor(); // Required for the helper to work
builder.Services.AddScoped<Afri.Utilities.CurrentUser>(); // Register our helper

// 5. Authentication: cookie-based
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Signin";
        options.Cookie.Name = "AfriAuth";
        options.AccessDeniedPath = "/Users/AccessDenied";
    });

// 6. Optional: named policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireInstructor", policy => policy.RequireRole("Instructor"));
});

var app = builder.Build();

// Ensure TopicMaterials table exists (avoids "Invalid object name" if script wasn't run)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AFRILEARNContext>().Database;
        db.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TopicMaterials')
            BEGIN
                CREATE TABLE [dbo].[TopicMaterials] (
                    [Id] INT IDENTITY(1,1) NOT NULL,
                    [TopicId] INT NOT NULL,
                    [Title] NVARCHAR(200) NOT NULL,
                    [ResourceType] NVARCHAR(50) NOT NULL,
                    [FilePath] NVARCHAR(500) NULL,
                    [ExternalUrl] NVARCHAR(1000) NULL,
                    [FileName] NVARCHAR(255) NULL,
                    [ContentType] NVARCHAR(100) NULL,
                    [FileSizeBytes] BIGINT NULL,
                    [UploadedByUserId] INT NOT NULL,
                    [CreatedDate] DATETIME NULL DEFAULT (getdate()),
                    [ModifiedDate] DATETIME NULL,
                    CONSTRAINT [PK_TopicMaterials] PRIMARY KEY ([Id]),
                    CONSTRAINT [FK_TopicMaterials_Topics] FOREIGN KEY ([TopicId]) REFERENCES [dbo].[Topics]([Id]) ON DELETE CASCADE,
                    CONSTRAINT [FK_TopicMaterials_Users] FOREIGN KEY ([UploadedByUserId]) REFERENCES [dbo].[Users]([Id])
                );
                CREATE NONCLUSTERED INDEX [IX_TopicMaterials_TopicId] ON [dbo].[TopicMaterials]([TopicId]);
            END
        ");
    }
    catch (Exception)
    {
        // Table may already exist or DB not ready; continue
    }
}

// 7. Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 8. Enable Sessions (MUST be placed between UseRouting and UseAuthorization)
app.UseSession();

// 9. Must call Authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

// 10. Define Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
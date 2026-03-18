using Afri.Models;
using Afri.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afri.Controllers;

public class TopicMaterialsController : Controller
{
    private readonly AFRILEARNContext _context;
    private readonly CurrentUser _currentUser;
    private readonly IWebHostEnvironment _env;
    private const int MaxFileSizeBytes = 50 * 1024 * 1024; // 50 MB
    private static readonly string[] AllowedDocumentExtensions = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".txt" };
    private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
    private static readonly string[] AllowedVideoExtensions = { ".mp4", ".webm", ".ogg", ".mov" };

    public TopicMaterialsController(AFRILEARNContext context, CurrentUser currentUser, IWebHostEnvironment env)
    {
        _context = context;
        _currentUser = currentUser;
        _env = env;
    }

    /// <summary>List materials for a topic. Students can view/download; instructors can also upload/delete.</summary>
    public async Task<IActionResult> Index(int topicId)
    {
        var topic = await _context.Topics
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == topicId);
        if (topic == null) return NotFound();

        var materials = await _context.TopicMaterials
            .Include(m => m.UploadedByUser)
            .Where(m => m.TopicId == topicId)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();

        ViewBag.Topic = topic;
        return View(materials);
    }

    /// <summary>Upload form (instructor only).</summary>
    [HttpGet]
    public async Task<IActionResult> Create(int topicId)
    {
        if (!_currentUser.IsInstructor()) return Forbid();

        var topic = await _context.Topics.Include(t => t.Subject).FirstOrDefaultAsync(t => t.Id == topicId);
        if (topic == null) return NotFound();

        ViewBag.Topic = topic;
        return View(new TopicMaterial { TopicId = topicId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int topicId, string title, string resourceType, IFormFile? file, string? externalUrl)
    {
        if (!_currentUser.IsInstructor()) return Forbid();

        var topic = await _context.Topics.Include(t => t.Subject).FirstOrDefaultAsync(t => t.Id == topicId);
        if (topic == null) return NotFound();

        title = title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            TempData["Error"] = "Title is required.";
            ViewBag.Topic = topic;
            return View(new TopicMaterial { TopicId = topicId });
        }

        resourceType = resourceType ?? "Document";
        if (resourceType != "Document" && resourceType != "Video" && resourceType != "Image")
            resourceType = "Document";

        string? filePath = null;
        string? fileName = null;
        string? contentType = null;
        long? fileSize = null;

        if (!string.IsNullOrWhiteSpace(externalUrl))
        {
            externalUrl = externalUrl.Trim();
            if (externalUrl.Length > 1000)
            {
                TempData["Error"] = "External URL is too long.";
                ViewBag.Topic = topic;
                return View(new TopicMaterial { TopicId = topicId, Title = title, ResourceType = resourceType, ExternalUrl = externalUrl });
            }
        }
        else if (file != null && file.Length > 0)
        {
            var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant() ?? "";
            var allowed = resourceType == "Document" ? AllowedDocumentExtensions
                : resourceType == "Image" ? AllowedImageExtensions
                : AllowedVideoExtensions;
            if (!allowed.Contains(ext))
            {
                TempData["Error"] = $"Allowed file types for {resourceType}: {string.Join(", ", allowed)}";
                ViewBag.Topic = topic;
                return View(new TopicMaterial { TopicId = topicId, Title = title, ResourceType = resourceType });
            }
            if (file.Length > MaxFileSizeBytes)
            {
                TempData["Error"] = $"File size must be under {MaxFileSizeBytes / (1024 * 1024)} MB.";
                ViewBag.Topic = topic;
                return View(new TopicMaterial { TopicId = topicId, Title = title, ResourceType = resourceType });
            }

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "topicmaterials");
            Directory.CreateDirectory(uploadsDir);
            var safeName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsDir, safeName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            filePath = "/uploads/topicmaterials/" + safeName;
            fileName = file.FileName;
            contentType = file.ContentType;
            fileSize = file.Length;
        }
        else if (resourceType != "Video" || string.IsNullOrWhiteSpace(externalUrl))
        {
            TempData["Error"] = "Please upload a file or enter an external URL (for videos).";
            ViewBag.Topic = topic;
            return View(new TopicMaterial { TopicId = topicId, Title = title, ResourceType = resourceType, ExternalUrl = externalUrl });
        }

        var userId = _currentUser.GetUserId();
        if (userId == 0) return RedirectToAction("Signin", "Users");

        var material = new TopicMaterial
        {
            TopicId = topicId,
            Title = title,
            ResourceType = resourceType,
            FilePath = filePath,
            ExternalUrl = string.IsNullOrWhiteSpace(externalUrl) ? null : externalUrl,
            FileName = fileName,
            ContentType = contentType,
            FileSizeBytes = fileSize,
            UploadedByUserId = userId,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        _context.TopicMaterials.Add(material);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Material added successfully.";
        return RedirectToAction(nameof(Index), new { topicId });
    }

    /// <summary>Watch video embedded on the platform (YouTube/Vimeo).</summary>
    public async Task<IActionResult> Watch(int id)
    {
        var material = await _context.TopicMaterials
            .Include(m => m.Topic).ThenInclude(t => t!.Subject)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (material == null) return NotFound();

        string? embedUrl = null;
        if (!string.IsNullOrEmpty(material.ExternalUrl) && material.ResourceType == "Video")
            embedUrl = GetVideoEmbedUrl(material.ExternalUrl);

        ViewBag.EmbedUrl = embedUrl;
        ViewBag.OriginalUrl = material.ExternalUrl;
        return View(material);
    }

    private static string? GetVideoEmbedUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        var u = url.Trim();
        // YouTube: watch?v=ID, youtu.be/ID, embed/ID
        if (u.Contains("youtube.com", StringComparison.OrdinalIgnoreCase) || u.Contains("youtu.be", StringComparison.OrdinalIgnoreCase))
        {
            var id = "";
            if (u.Contains("youtu.be/", StringComparison.OrdinalIgnoreCase))
            {
                var i = u.IndexOf("youtu.be/", StringComparison.OrdinalIgnoreCase) + 9;
                id = i < u.Length ? u[i..].Split('?', '&')[0] : "";
            }
            else if (u.Contains("v="))
            {
                var start = u.IndexOf("v=", StringComparison.OrdinalIgnoreCase) + 2;
                var end = u.IndexOf('&', start);
                id = end > 0 ? u[start..end] : u[start..];
            }
            if (!string.IsNullOrEmpty(id))
                return $"https://www.youtube.com/embed/{id}?rel=0";
        }
        // Vimeo: vimeo.com/ID or player.vimeo.com/video/ID
        if (u.Contains("vimeo.com", StringComparison.OrdinalIgnoreCase))
        {
            var id = "";
            var lastSlash = u.LastIndexOf('/');
            if (lastSlash >= 0 && lastSlash < u.Length - 1)
                id = u[(lastSlash + 1)..].Split('?')[0];
            if (!string.IsNullOrEmpty(id) && id.All(char.IsDigit))
                return $"https://player.vimeo.com/video/{id}";
        }
        return null;
    }

    /// <summary>Download file (students and instructors).</summary>
    public async Task<IActionResult> Download(int id)
    {
        var material = await _context.TopicMaterials
            .Include(m => m.Topic)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (material == null) return NotFound();

        if (string.IsNullOrEmpty(material.FilePath))
        {
            if (!string.IsNullOrEmpty(material.ExternalUrl))
                return Redirect(material.ExternalUrl);
            return NotFound();
        }

        var fullPath = Path.Combine(_env.WebRootPath, material.FilePath.TrimStart('/'));
        if (!System.IO.File.Exists(fullPath)) return NotFound();

        var fileName = material.FileName ?? Path.GetFileName(fullPath) ?? "download";
        return PhysicalFile(fullPath, material.ContentType ?? "application/octet-stream", fileName);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!_currentUser.IsInstructor()) return Forbid();

        var material = await _context.TopicMaterials.FirstOrDefaultAsync(m => m.Id == id);
        if (material == null) return NotFound();

        var topicId = material.TopicId;
        if (!string.IsNullOrEmpty(material.FilePath))
        {
            var fullPath = Path.Combine(_env.WebRootPath, material.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
                try { System.IO.File.Delete(fullPath); } catch { /* ignore */ }
        }

        _context.TopicMaterials.Remove(material);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Material removed.";
        return RedirectToAction(nameof(Index), new { topicId });
    }
}

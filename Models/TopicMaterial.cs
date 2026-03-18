using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

/// <summary>
/// Instructor-uploaded materials (documents, videos, images) per topic.
/// Students can view and download. No changes to existing tables.
/// </summary>
public class TopicMaterial
{
    [Key]
    public int Id { get; set; }

    public int TopicId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    /// <summary>Document, Video, or Image</summary>
    [StringLength(50)]
    public string ResourceType { get; set; } = null!;

    /// <summary>Relative path for uploaded file (e.g. /uploads/topicmaterials/guid.pdf)</summary>
    [StringLength(500)]
    public string? FilePath { get; set; }

    /// <summary>External URL for video/image (e.g. YouTube, Vimeo)</summary>
    [StringLength(1000)]
    public string? ExternalUrl { get; set; }

    [StringLength(255)]
    public string? FileName { get; set; }

    [StringLength(100)]
    public string? ContentType { get; set; }

    public long? FileSizeBytes { get; set; }

    public int UploadedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("TopicId")]
    [InverseProperty("TopicMaterials")]
    public virtual Topic Topic { get; set; } = null!;

    [ForeignKey("UploadedByUserId")]
    [InverseProperty("TopicMaterials")]
    public virtual User UploadedByUser { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace Source.Domain;

/// <summary>
/// A base entity that all other entities should inherit from
/// </summary>
public class EntityBase
{
    /// <summary>
    /// The unique identifier for this entity.
    /// </summary>
    [Key]
    public uint Id { get; set; }
    /// <summary>
    /// The date and time this entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// The date and time this entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    /// <summary>
    /// The date and time this entity was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    /// <summary>
    /// When true, this entity is soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
    /// <summary>
    /// Who created this entity.
    /// </summary>
    public string CreatedBy { get; set; }
    /// <summary>
    /// Who last updated this entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
    /// <summary>
    /// Who deleted this entity.
    /// </summary>
    public string? DeletedBy { get; set; }
}
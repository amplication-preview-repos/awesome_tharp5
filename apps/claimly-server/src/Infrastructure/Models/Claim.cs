using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claimly.Infrastructure.Models;

[Table("Claims")]
public class ClaimDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    public string? ClaimStatusId { get; set; }

    [ForeignKey(nameof(ClaimStatusId))]
    public ClaimStatusDbModel? ClaimStatus { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    public DateTime? Date { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? PolicyId { get; set; }

    [ForeignKey(nameof(PolicyId))]
    public PolicyDbModel? Policy { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

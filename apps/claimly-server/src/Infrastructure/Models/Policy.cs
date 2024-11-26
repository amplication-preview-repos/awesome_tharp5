using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claimly.Infrastructure.Models;

[Table("Policies")]
public class PolicyDbModel
{
    public List<ClaimDbModel>? Claims { get; set; } = new List<ClaimDbModel>();

    [Range(-999999999, 999999999)]
    public double? CoverageAmount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? PolicyNumber { get; set; }

    [Range(-999999999, 999999999)]
    public double? Premium { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

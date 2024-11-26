using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claimly.Infrastructure.Models;

[Table("ClaimStatuses")]
public class ClaimStatusDbModel
{
    public List<ClaimDbModel>? Claims { get; set; } = new List<ClaimDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

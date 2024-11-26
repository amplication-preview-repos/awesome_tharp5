using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claimly.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    public List<ClaimDbModel>? Claims { get; set; } = new List<ClaimDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    public List<PolicyDbModel>? Policies { get; set; } = new List<PolicyDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

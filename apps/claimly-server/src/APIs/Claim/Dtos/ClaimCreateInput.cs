namespace Claimly.APIs.Dtos;

public class ClaimCreateInput
{
    public double? Amount { get; set; }

    public ClaimStatus? ClaimStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public Policy? Policy { get; set; }

    public DateTime UpdatedAt { get; set; }
}

namespace Claimly.APIs.Dtos;

public class ClaimWhereInput
{
    public double? Amount { get; set; }

    public string? ClaimStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Policy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

namespace Claimly.APIs.Dtos;

public class PolicyCreateInput
{
    public List<Claim>? Claims { get; set; }

    public double? CoverageAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public string? PolicyNumber { get; set; }

    public double? Premium { get; set; }

    public DateTime UpdatedAt { get; set; }
}

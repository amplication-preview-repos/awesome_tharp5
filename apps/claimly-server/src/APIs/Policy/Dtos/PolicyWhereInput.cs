namespace Claimly.APIs.Dtos;

public class PolicyWhereInput
{
    public List<string>? Claims { get; set; }

    public double? CoverageAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public string? PolicyNumber { get; set; }

    public double? Premium { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

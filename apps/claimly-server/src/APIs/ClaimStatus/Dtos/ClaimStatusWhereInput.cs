namespace Claimly.APIs.Dtos;

public class ClaimStatusWhereInput
{
    public List<string>? Claims { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

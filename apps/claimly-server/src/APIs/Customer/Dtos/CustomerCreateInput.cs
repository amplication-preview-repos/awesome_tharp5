namespace Claimly.APIs.Dtos;

public class CustomerCreateInput
{
    public List<Claim>? Claims { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public List<Policy>? Policies { get; set; }

    public DateTime UpdatedAt { get; set; }
}

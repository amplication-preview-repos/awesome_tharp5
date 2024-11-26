namespace Claimly.APIs.Dtos;

public class CustomerUpdateInput
{
    public List<string>? Claims { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public List<string>? Policies { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

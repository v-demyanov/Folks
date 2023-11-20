using System.ComponentModel.DataAnnotations;

public record class InputModel
{
    [Required]
    public required string UserName { get; init; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public required string Password { get; init; }
}
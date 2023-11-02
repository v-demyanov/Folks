using System.ComponentModel.DataAnnotations;

public record class InputModel
{
    [Required]
    public required string UserName { get; init; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; init; }
}
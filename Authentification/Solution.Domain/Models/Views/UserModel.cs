using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Solution.Domain.Models.Views;

public class UserModel
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Solution.Domain.Models.Request.Security;

public class LoginRequestModel
{
    [Required]
    [JsonPropertyName("email")]

    public string Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}

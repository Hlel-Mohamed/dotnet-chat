using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_chat.Models;

public class User
{
    public int Id { get; set; }
    [StringLength(255)]
    public string? Name { get; set; }
    [StringLength(200)]
    [EmailAddress]
    public string? Email { get; set; }
    [StringLength(200)]
    [JsonIgnore]
    public string? Password { get; set; }
}
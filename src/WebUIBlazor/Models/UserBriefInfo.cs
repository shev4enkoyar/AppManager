using System.Text.Json.Serialization;

namespace WebUIBlazor.Models;

public class UserBriefInfo
{
    [JsonPropertyName("userName")] public string UserName { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("role")] public List<string> Role { get; set; }
}

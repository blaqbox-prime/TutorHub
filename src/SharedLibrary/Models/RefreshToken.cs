namespace SharedLibrary.Models;

public class RefreshToken
{
  public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(14);
    public bool IsExpired => DateTime.UtcNow >= Expires;
}

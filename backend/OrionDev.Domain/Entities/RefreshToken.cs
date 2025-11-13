namespace OrionDev.Domain.Entities;

public class RefreshToken {
  public Guid Id { get; init; }

  public string Token { get; init; }

  public DateTime Expires { get; init; }

  public Guid UserId { get; init; }

  public bool IsValid => DateTime.UtcNow < Expires;

  private RefreshToken() {
  }

  public RefreshToken(Guid userId, string token, DateTime expires) {
    Id = Guid.NewGuid();
    Token = token;
    Expires = expires;
    UserId = userId;
  }
}

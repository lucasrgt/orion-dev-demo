namespace OrionDev.Domain.Interfaces;

public interface IJwtService {
  string CreateAccessToken(string id, string role);
}

namespace OrionDev.Domain.Interfaces;

public interface IPasswordEncryptionService {
  string Hash(string plainTextPassword);
  bool Verify(string hashedPassword, string plainTextPassword);
}

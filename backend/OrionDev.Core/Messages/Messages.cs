namespace OrionDev.Core.Messages;

public static class Messages {
  // Common
  public const string IdNotFound = "Id não encontrado.";

  public const string InvalidEmail = "E-mail inválido.";
  public const string EmailNotFound = "Email não encontrado.";

  public const string InvalidCredentials = "Credenciais inválidas.";

  public const string AtLeastOneFieldToUpdate =
    "Pelo menos um campo deve ser providenciado para atualizar.";

  // Auth
  public const string InsecurePassword =
    "Senha inválida. A senha deve conter pelo menos uma letra maíuscula, uma letra minúscula, um número e um caractere especial.";

  // Refresh Token
  public const string RefreshTokenNotFound = "Refresh token não encontrado.";

  // Entities
  // Mission
  public const string MissionCannotHavePlanetAndMoon =
    "A missão não pode estar vinculada simultaneamente a um planeta e uma lua.";

  public const string MissionMustHavePlanetOrMoon =
    "A missão deve estar vinculada a um planeta ou a uma lua.";

  // VOs
  // Color Palette
  public const string InvalidPrimaryColorHex =
    "Formato inválido para a cor primária (hexadecimal esperado).";

  public const string InvalidSecondaryColorHex =
    "Formato inválido para a cor secundária (hexadecimal esperado).";

  public const string EmptyPrimaryGradient = "O gradiente primário deve conter ao menos uma cor.";

  public const string InvalidGradientColorHex =
    "Uma ou mais cores do gradiente possuem formato hexadecimal inválido.";

  public const string ColorPaletteIsRequired = "A paleta de cores é obrigatória.";
}

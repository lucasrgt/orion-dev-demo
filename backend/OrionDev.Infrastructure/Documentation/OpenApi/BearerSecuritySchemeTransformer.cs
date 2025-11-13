using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OrionDev.Infrastructure.Settings;

namespace OrionDev.Infrastructure.Documentation.OpenApi;

public sealed class BearerSecuritySchemeTransformer(
  IAuthenticationSchemeProvider authenticationSchemeProvider,
  IOptions<OpenApiSettings> openApiSettings)
  : IOpenApiDocumentTransformer {
  public async Task TransformAsync(OpenApiDocument document,
    OpenApiDocumentTransformerContext context,
    CancellationToken cancellationToken) {
    var authenticationSchemes =
      await authenticationSchemeProvider.GetAllSchemesAsync();

    if (authenticationSchemes.All(scheme => scheme.Name != "Bearer")) {
      return;
    }

    document.Components ??= new OpenApiComponents();

    document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme {
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      In = ParameterLocation.Header,
      BearerFormat = "JWT",
      Description = "JWT Authorization header using the Bearer scheme."
    };

    var publicPaths = openApiSettings.Value.PublicEndpoints ?? [];

    foreach (var (path, pathItem) in document.Paths) {
      if (publicPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase))) {
        continue;
      }

      foreach (var operation in pathItem.Operations.Values) {
        operation.Security.Add(new OpenApiSecurityRequirement {
          [new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
              Id = "Bearer",
              Type = ReferenceType.SecurityScheme
            }
          }] = Array.Empty<string>()
        });
      }
    }
  }
}

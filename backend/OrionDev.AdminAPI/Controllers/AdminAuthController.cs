using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Auth;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Interfaces;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminAuthController(
  IJwtService jwtService,
  IRefreshTokenService refreshTokenService,
  Login loginUc
) : ControllerBase {
  // POST: api/AdminAuth/login
  /// <summary>
  /// Logs in the user and issues an access token and a refresh token.
  /// </summary>
  /// <param name="request">Login input containing credentials.</param>
  /// <returns>Returns the access token if login is successful.</returns>
  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(LoginInput request) {
    try {
      var output = await loginUc.Execute(request);

      // Tokens/Session
      var accessToken = jwtService.CreateAccessToken(
        output.Id,
        output.Role
      );

      var refreshToken = await refreshTokenService.CreateRefreshToken(output.Id);
      SetRefreshToken(refreshToken);
      return Ok(accessToken);
    } catch (AuthenticationException e) {
      return BadRequest(e.Message);
    }
  }

  // POST: api/Auth/logout
  /// <summary>
  /// Logs out the user by invalidating the refresh token and removing the refresh token cookie.
  /// </summary>
  /// <returns>Returns a message indicating the user was logged out successfully.</returns>
  [HttpPost("logout")]
  public async Task<IActionResult> Logout() {
    var refreshToken = Request.Cookies["refreshToken"];

    if (string.IsNullOrEmpty(refreshToken)) {
      return BadRequest("No refresh token provided.");
    }

    await refreshTokenService.InvalidateRefreshToken(refreshToken);
    RemoveRefreshTokenCookie();

    return Ok("Logged out successfully.");
  }

  // POST: api/Auth/refresh
  /// <summary>
  /// Refreshes the user's access token using the refresh token stored in the cookie.
  /// </summary>
  /// <returns>Returns a new access token if the refresh token is valid.</returns>
  [HttpPost("refresh")]
  public async Task<ActionResult<string>> RefreshToken() {
    var refreshToken = Request.Cookies["refreshToken"];
    if (string.IsNullOrEmpty(refreshToken)) {
      return Unauthorized("No refresh token provided.");
    }

    var isValid = await refreshTokenService.ValidateRefreshToken(refreshToken);
    if (!isValid) {
      return Unauthorized("Invalid or expired refresh token.");
    }

    var user = await refreshTokenService.GetUserByRefreshToken(refreshToken);

    var newAccessToken = jwtService.CreateAccessToken(
      user.Id.ToString(),
      user.Role.ToString()
    );

    return Ok(newAccessToken);
  }

  /// <summary>
  /// Sets the refresh token in an HTTP-only cookie.
  /// </summary>
  /// <param name="newRefreshToken">The refresh token to be set in the cookie.</param>
  private void SetRefreshToken(RefreshToken newRefreshToken) {
    var cookieOptions = new CookieOptions { HttpOnly = true, Expires = newRefreshToken.Expires };

    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
  }


  /// <summary>
  /// Removes the refresh token from the cookie by setting its expiration date to the past.
  /// </summary>
  private void RemoveRefreshTokenCookie() {
    if (Request.Cookies["refreshToken"] != null) {
      Response.Cookies.Append(
        "refreshToken",
        "",
        new CookieOptions {
          HttpOnly = false, // TODO: Change to true when deploying to production (HTTPS BACK/FRONT)
          Secure = false, // TODO: Change to true when deploying to production (HTTPS BACK/FRONT)
          SameSite = SameSiteMode.None,
          Expires = DateTime.UtcNow.AddDays(-1)
        }
      );
    }
  }
}

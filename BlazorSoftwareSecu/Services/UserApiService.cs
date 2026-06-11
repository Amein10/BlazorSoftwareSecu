using Microsoft.AspNetCore.Http;

namespace BlazorSoftwareSecu.Services;

public class UserApiService
{
    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserApiService(
        IHttpClientFactory factory,
        IHttpContextAccessor httpContextAccessor)
    {
        _factory = factory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool Success, string Message)> DeleteUserAsync(string userId)
    {
        var client = _factory.CreateClient("SoftwareSecuApi");

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"api/users/{userId}");

        var cookie = _httpContextAccessor.HttpContext?
            .Request
            .Headers
            .Cookie
            .ToString();

        if (!string.IsNullOrEmpty(cookie))
        {
            request.Headers.Add("Cookie", cookie);
        }

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (true, content);
        }

        return (false, $"API-fejl: {(int)response.StatusCode} - {content}");
    }
}
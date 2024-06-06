namespace WebUIBlazor.Services.ContentApi;

public class ContentApi : IContentApi
{
    private readonly HttpClient _httpClient;

    public ContentApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task GetProjectList()
    {
        using var response = await _httpClient.GetAsync("");

        response.EnsureSuccessStatusCode();

        var some = response.Content.ToString();
    }
}

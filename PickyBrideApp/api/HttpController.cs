using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace PickyBride.api;

public class HttpController
{
    private readonly HttpClient _client;
    private readonly string _emulatorUri;
    private readonly string _emulatorSession;

    public HttpController()
    {
        _client = new HttpClient();
        _emulatorUri = System.Configuration.ConfigurationManager.AppSettings["EmulatorUri"]!;
        _emulatorSession = System.Configuration.ConfigurationManager.AppSettings["EmulatorSession"]!;
    }
    
    private Uri CreateUri(string requestUri)
    {
        var uri = new Uri($"{_emulatorUri}{requestUri}");
        var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

        httpValueCollection.Remove("session");
        httpValueCollection.Add("session", _emulatorSession);

        var ub = new UriBuilder(uri)
        {
            Query = httpValueCollection.ToString()
        };

        return ub.Uri;
    }
    
    public async Task<T?> SendPostRequest<T, TR>(string requestUri, TR? content)
    {
        HttpContent? httpContent = null;
        if (content != null)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(content);
            httpContent = new ByteArrayContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
        
        var response = await _client.PostAsync(CreateUri(requestUri), httpContent);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString);
    }

    public async Task<T?> SendPostRequest<T>(string requestUri)
    {
        return await SendPostRequest<T, string>(requestUri, null);
    }
}
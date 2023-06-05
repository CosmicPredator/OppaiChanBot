using System.Diagnostics;
using Newtonsoft.Json;
using OppaiChanBot.Helpers;

namespace OppaiChanBot.Services;

public class WaifuService : IWaifuService
{
    private readonly HttpClient _httpClient;
    private IWaifuService _waifuServiceImplementation;

    public WaifuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string?> GetNsfwImageStream(NsfwTags tag)
    {
        string baseUrl = $"https://waifu.pics/api/nsfw/{tag.ToString().ToLower()}";
        HttpResponseMessage response = await _httpClient.GetAsync(baseUrl);
        ImageModel? imageUrl = JsonConvert.DeserializeObject<ImageModel>
            (await response.Content.ReadAsStringAsync());
        return imageUrl!.Url;
    }

    public async Task<string?> GetSfwImageStream(SfwTags tag)
    {
        string baseUrl = $"https://waifu.pics/api/sfw/{tag.ToString().ToLower()}";
        HttpResponseMessage response = await _httpClient.GetAsync(baseUrl);
        ImageModel? imageUrl = JsonConvert.DeserializeObject<ImageModel>
            (await response.Content.ReadAsStringAsync());
        return imageUrl!.Url;
    }

    public async Task<long> GetApiLatency()
    {
        string baseUrl = $"https://waifu.pics/api/sfw/neko";
        var stopWatch = Stopwatch.StartNew();
        await _httpClient.GetAsync(baseUrl);
        stopWatch.Stop();
        return stopWatch.ElapsedMilliseconds;
    }
}

public class ImageModel
{
    [JsonProperty("url")]
    public string? Url { get; set; }
}   
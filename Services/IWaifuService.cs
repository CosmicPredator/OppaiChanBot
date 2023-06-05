using OppaiChanBot.Helpers;

namespace OppaiChanBot.Services;

public interface IWaifuService
{
    public Task<string?> GetNsfwImageStream(NsfwTags tag);
    public Task<string?> GetSfwImageStream(SfwTags tag);
    public Task<long> GetApiLatency();
}
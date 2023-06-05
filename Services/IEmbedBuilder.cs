using Discord;

namespace OppaiChanBot.Services;

public interface IEmbedBuilder
{
    public Embed? BuildEmbed(string? imageUrl);
}
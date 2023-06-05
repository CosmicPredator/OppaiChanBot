using Discord;

namespace OppaiChanBot.Services;

public class EmbedBuilder : IEmbedBuilder
{
    public Embed? BuildEmbed(string? imageUrl)
    {
        return new Discord.EmbedBuilder()
            .WithImageUrl(imageUrl)
            .WithTitle(Path.GetFileName(imageUrl))
            .WithColor(Color.Green)
            .WithCurrentTimestamp()
            .Build();
    }
}
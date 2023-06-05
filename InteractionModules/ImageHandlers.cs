using Discord.Interactions;
using OppaiChanBot.Helpers;
using OppaiChanBot.Services;

namespace OppaiChanBot.InteractionModules;

public class ImageHandlers : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IWaifuService _waifuService;
    private readonly IEmbedBuilder _embedBuilder;

    public ImageHandlers(IWaifuService waifuService, IEmbedBuilder embedBuilder)
    {
        _waifuService = waifuService;
        _embedBuilder = embedBuilder;
    }
    
    [SlashCommand("nsfw", "Get Random NSFW Waifu Pic.")]
    [RequireNsfw]
    public async Task HandleNsfwImageCommand(NsfwTags tag)
    {
        await RespondAsync(
            embed: _embedBuilder.BuildEmbed(await _waifuService.GetNsfwImageStream(tag)));
    }

    [SlashCommand("sfw", "Get Random SFW Waifu Pic.")]
    public async Task HandleSfwImageCommand(SfwTags tag)
    {
        await RespondAsync(
            embed: _embedBuilder.BuildEmbed(await _waifuService.GetSfwImageStream(tag)));
    }
}
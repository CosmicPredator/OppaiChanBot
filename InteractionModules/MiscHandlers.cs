using Discord;
using Discord.Interactions;
using OppaiChanBot.Helpers;
using OppaiChanBot.Services;

namespace OppaiChanBot.InteractionModules;

public class MiscHandlers: InteractionModuleBase<SocketInteractionContext>
{
    private readonly IWaifuService _waifuService;

    public MiscHandlers(IWaifuService waifuService)
    {
        _waifuService = waifuService;
    }

    [SlashCommand("ping", "Check the current ping of the Bot")]
    [RequireNsfw]
    public async Task HandlePingCommand()
    {
        int ping = Context.Client.Latency;
        long apiLatency = await _waifuService.GetApiLatency();

        List<EmbedFieldBuilder> fieldBuilder = new()
        {
            new EmbedFieldBuilder()
                .WithName("Bot Latency")
                .WithValue($"`{ping}ms`")
                .WithIsInline(true),
            new EmbedFieldBuilder()
                .WithName("API Latency")
                .WithValue($"`{apiLatency}`ms")
                .WithIsInline(true)
        };

        var eb = new Discord.EmbedBuilder()
            .WithTitle("Ping Stats")
            .WithFields(fieldBuilder)
            .WithColor(Color.Green)
            .Build();

        await RespondAsync(embed: eb, ephemeral: true);
    }
}
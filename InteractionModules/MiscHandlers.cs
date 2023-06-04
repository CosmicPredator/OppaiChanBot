using Discord.Interactions;

namespace OppaiChanBot.InteractionModules;

public class MiscHandlers: InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Check the current ping of the Bot")]
    public async Task HandlePingCommand()
    {
        int ping = Context.Client.Latency;
        await RespondAsync($"`ping: {ping.ToString()}ms`", ephemeral: true);
    }
}
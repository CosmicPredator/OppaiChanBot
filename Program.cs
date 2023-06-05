using Discord;
using Discord.Interactions;
using OppaiChanBot.Helpers;
using OppaiChanBot.Services;
using EmbedBuilder = OppaiChanBot.Services.EmbedBuilder;

namespace OppaiChanBot;

public class Program
{
    private readonly IServiceProvider _services;

    private Program() => _services = ConfigureServices();
    
    public static void Main() 
        => new Program().MainAsync().GetAwaiter().GetResult();
    
    private async Task MainAsync()
    {
        DiscordSocketClient client = _services.GetRequiredService<DiscordSocketClient>();
        InteractionService sCommands = _services.GetRequiredService<InteractionService>();

        await _services.GetRequiredService<InteractionHandler>().InitializeAsync();

        client.Ready += async () =>
        {
            await sCommands.RegisterCommandsToGuildAsync(1112404779252064289);
            await Task.CompletedTask;
        };
        
        client.Log += async message =>
        {
            Console.WriteLine(message.ToString());
            await Task.CompletedTask;
        };
        
        await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }

    private static IServiceProvider ConfigureServices()
    {
        DiscordSocketConfig config = new()
        {
            GatewayIntents = GatewayIntents.MessageContent | GatewayIntents.AllUnprivileged,
            LogGatewayIntentWarnings = true,
            LogLevel = LogSeverity.Info
        };

        var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => 
                new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandler>()
            .AddHttpClient()
            .AddSingleton<IWaifuService, WaifuService>()
            .AddSingleton<IEmbedBuilder, EmbedBuilder>();

        return collection.BuildServiceProvider();
    }
}
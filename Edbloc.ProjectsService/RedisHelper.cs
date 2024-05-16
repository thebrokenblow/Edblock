using StackExchange.Redis;

namespace Edbloc.ProjectsService;

public class RedisHelper
{
    private static IDatabase redisDatabase;

    public static IDatabase RedisDatabase
    {
        get => redisDatabase;
    }

    static RedisHelper()
    {
        var connections = ConnectionMultiplexer.Connect("localhost:6379");
        redisDatabase = connections.GetDatabase();
    }
}
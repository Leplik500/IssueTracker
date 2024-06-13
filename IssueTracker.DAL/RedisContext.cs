using StackExchange.Redis;

namespace IssueTracker.DAL;

public class RedisContext : IDisposable {
    private readonly ConnectionMultiplexer _connectionMultiplexer;
    public RedisContext(ConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public IDatabaseAsync Database => _connectionMultiplexer.GetDatabase();
    public void Dispose()
    {
        _connectionMultiplexer.Dispose();
    }


}
using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using StackExchange.Redis;

namespace IssueTracker.DAL.Repositories;

public class EmojiRepository : IRedisRepository {

    private readonly RedisContext _redisContext;

    public EmojiRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public async Task Create(String key, String value)
    {
        await _redisContext.Database.StringSetAsync(key, value);
    }
    public async Task<RedisValue> Get(String key)
    {
        return await _redisContext.Database.StringGetAsync(key);
    }
    public async Task Delete(String key)
    {
        await _redisContext.Database.KeyDeleteAsync(key);
    }
    public async Task<RedisValue> Update(String key, String value)
    {
        return await _redisContext.Database.StringGetSetAsync(key, value);
    }
}
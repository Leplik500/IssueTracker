using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using StackExchange.Redis;

namespace IssueTracker.DAL.Repositories;

public class EmojiRepository : IRedisRepository<EmojiEntity> {

    private readonly RedisContext _redisContext;

    public EmojiRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public async Task Create(EmojiEntity entity)
    {
        await _redisContext.Database.SetAddAsync(entity.Shortcode, entity.Emoji);
    }
    public async Task<RedisValue> Get(String shortcode)
    {
        return await _redisContext.Database.StringGetAsync(shortcode);
    }
    public async Task Delete(String shortcode)
    {
        await _redisContext.Database.KeyDeleteAsync(shortcode);
    }
    public async Task<RedisValue> Update(EmojiEntity entity)
    {
        return await _redisContext.Database.StringGetSetAsync(entity.Shortcode, entity.Emoji);
    }
}
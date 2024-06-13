using StackExchange.Redis;

namespace IssueTracker.DAL.Interfaces;

public interface IRedisRepository {
    Task Create(String key, String value);
    Task<RedisValue> Get(String key);
    Task Delete(String key);
    Task<RedisValue> Update(String key, String value);

}
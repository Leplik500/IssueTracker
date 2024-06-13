using StackExchange.Redis;

namespace IssueTracker.DAL.Interfaces;

public interface IRedisRepository<T> {
    Task Create(T entity);
    Task<RedisValue> Get(String key);
    Task Delete(String key);
    Task<RedisValue> Update(T entity);

}
using StackExchange.Redis;

namespace Infrastructure.Redis;

public interface IOnlineUserService
{
    Task MarkOnline(string userId);
    Task MarkOffline(string userId);
    Task<long> GetOnlineCountAsync();
}

public class OnlineUserService(IConnectionMultiplexer mux) : IOnlineUserService
{
    private readonly IDatabase _redis = mux.GetDatabase();
    private const string OnlineKey = "OnlineUsers";

    public async Task MarkOnline(string userId)
    {
        // 使用有序集合，score 为过期时间(Unix秒)
        double score = DateTimeOffset.Now.AddMinutes(30).ToUnixTimeSeconds();
        await _redis.SortedSetAddAsync(OnlineKey, userId, score);
    }

    public async Task MarkOffline(string userId)
    {
        await _redis.SortedSetRemoveAsync(OnlineKey, userId);
    }

    public async Task<long> GetOnlineCountAsync()
    {
        // 清除过期用户
        double now = DateTimeOffset.Now.ToUnixTimeSeconds();
        await _redis.SortedSetRemoveRangeByScoreAsync(OnlineKey, 0, now);
        return await _redis.SortedSetLengthAsync(OnlineKey);
    }
}
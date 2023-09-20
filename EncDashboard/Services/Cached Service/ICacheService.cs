namespace EncDashboard.Services.Cached_Service
{
    public interface ICacheService
    {
        void BulkRemove();
        TItem Get<TItem>(string cacheKey);
        void Remove(string key);
        void SetKey(string cacheKey, object value);
    }
}
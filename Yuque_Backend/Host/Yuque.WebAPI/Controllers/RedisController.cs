using Microsoft.AspNetCore.Mvc;
using Yuque.Infrastructure.Models;
using Yuque.Redis;

namespace Yuque.WebAPI.Controllers
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    /// <param name="redisService"></param>
    public class RedisController(IRedisService redisService) : BaseController
    {
        /// <summary>
        /// 获取redis所有keys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetListAsync(PagedQueryModelBase model)
        {
            return await redisService.ScanAsync(model);
        }
    }
}

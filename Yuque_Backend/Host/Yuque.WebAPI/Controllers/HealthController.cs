using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuque.Core.Attributes;

namespace Yuque.WebAPI.Controllers;

/// <summary>
/// Health Check Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HealthController() : BaseController
{
    /// <summary>
    /// 健康检测
    /// </summary>
    /// <returns></returns>
    [HttpGet("healthTips"), NoLogging]
    [AllowAnonymous]
    public async Task<int> HealthTips()
    {
        return 999;
    }
}

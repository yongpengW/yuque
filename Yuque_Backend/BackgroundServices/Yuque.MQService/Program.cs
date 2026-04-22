using Microsoft.AspNetCore.Builder;
using Yuque.Core;
using Yuque.Infrastructure.Enums;

var moduleKey = "yuque-mq";
var moduleTitle = "Yuque MQ Service";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle, CoreServiceType.MQService);
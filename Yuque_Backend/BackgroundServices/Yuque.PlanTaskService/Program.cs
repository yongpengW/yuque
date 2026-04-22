using Microsoft.AspNetCore.Builder;
using Yuque.Core;
using Yuque.Infrastructure.Enums;

var moduleKey = "yuque-plantask";
var moduleTitle = "Yuque Plan Task Service";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle, CoreServiceType.PlanTaskService);
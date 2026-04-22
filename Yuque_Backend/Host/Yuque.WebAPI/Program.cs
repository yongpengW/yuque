using Yuque.Core;

var moduleKey = "yuque_web_api";
var moduleTitle = "Yuque_Web_API";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);
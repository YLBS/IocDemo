using Common.Tool;
using IocDemo.Authentication.JWT;
using IocDemo.Extensions;
using IocDemo.Filter;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Model.ConfigOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



#region 全局异常过滤器

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
#endregion

//添加缓存
builder.Services.AddResponseCaching(); //只适用于get请求
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ITokenService, TokenService>();

//依赖注册接口和实现类
builder.Services.AddDi();

#region JWT验证授权
//初始化JwtAuthOptions
builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("JwtAuth"));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddJwtAuthorization();
#endregion

//实体映射配置
var mapper = AutoMapperConfig.ConfigureAutoMapper();
builder.Services.AddSingleton(mapper);

//Swagger 配置
builder.Services.AddSwaggerSetup();

//数据库配置
builder.Services.AddDbContexts(builder.Configuration);

// 跨域设置
CorsSetup.AddCorsSetup(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



app.UseCors(builder.Configuration.GetCorsOptions().Name);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var apiDescriptionGroups = app.Services.GetRequiredService<IApiDescriptionGroupCollectionProvider>().ApiDescriptionGroups.Items;
    foreach (var description in apiDescriptionGroups)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
    }
});

app.UseMiddleware<JwtMiddleware>();

// 认证
app.UseAuthentication();
// 授权
app.UseAuthorization();

app.MapControllers();

app.Run();

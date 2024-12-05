using Common.Tool;
using IocDemo.Authentication.JWT;
using IocDemo.Extensions;
using IocDemo.Filter;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Model.ConfigOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



#region ȫ���쳣������

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
#endregion

//��ӻ���
builder.Services.AddResponseCaching(); //ֻ������get����
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ITokenService, TokenService>();

//����ע��ӿں�ʵ����
builder.Services.AddDi();

#region JWT��֤��Ȩ
//��ʼ��JwtAuthOptions
builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("JwtAuth"));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddJwtAuthorization();
#endregion

//ʵ��ӳ������
var mapper = AutoMapperConfig.ConfigureAutoMapper();
builder.Services.AddSingleton(mapper);

//Swagger ����
builder.Services.AddSwaggerSetup();

//���ݿ�����
builder.Services.AddDbContexts(builder.Configuration);

// ��������
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

// ��֤
app.UseAuthentication();
// ��Ȩ
app.UseAuthorization();

app.MapControllers();

app.Run();

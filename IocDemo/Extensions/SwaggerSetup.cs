using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IocDemo.Extensions
{
    /// <summary>
    /// Swagger启动器
    /// </summary>
    public static class SwaggerSetup
    {
        /// <summary>
        /// Swagger配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var apiXml = Path.Combine(basePath, "IocDemo.xml");
                var iBusinessXml = Path.Combine(basePath, "Model.xml");
                options.IncludeXmlComments(apiXml, true);
                options.IncludeXmlComments(iBusinessXml, true);


                //options.TagActionsBy(api => api.GroupName);

                #region Swagger配置支持请求头参数传递

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "输入token,格式为 Bearer jwtToken",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"

                        }
                    },
                    new string[] { }
                }
            });

                #endregion
            });

        }

    }
}

using Model.ConfigOptions;

namespace IocDemo.Extensions
{
    /// <summary>
    /// 跨域配置
    /// </summary>
    public static class CorsSetup
    {
        /// <summary>
        /// 获取配置文件的跨域设置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static CorsOptions GetCorsOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("Cors").Get<CorsOptions>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="builder"></param>
        public static void AddCorsSetup(this WebApplicationBuilder builder)
        {
            var options = builder.Configuration.GetCorsOptions();
            builder.Services.AddCors(c =>
            {
                if (options.EnableAll)
                {
                    //允许任意跨域请求
                    c.AddPolicy(options.Name,
                        policy =>
                        {
                            policy
                                .SetIsOriginAllowed(host => true)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                }
                else
                {
                    c.AddPolicy(options.Name,
                        policy =>
                        {
                            policy
                                .WithOrigins(options.Policy.Select(x => x.Domain).ToArray())
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); ;
                        });
                }
            });
        }
    }
}

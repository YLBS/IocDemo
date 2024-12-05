using Entity.DemoDB;
using Microsoft.EntityFrameworkCore;

namespace IocDemo.Extensions
{
    /// <summary>
    /// 连接数据库配置
    /// </summary>
    public static class ContextConfig
    {
        /// <summary>
        /// 添加连接数据库字符串
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DemoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DemoConnection")));

        }
    }

}

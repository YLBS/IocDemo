using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IocDemo.Extensions
{
    /// <summary>
    /// Swagger Doc 生成配置
    /// </summary>
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiDescriptionGroupCollectionProvider provider;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="provider"></param>
        public SwaggerConfigureOptions(IApiDescriptionGroupCollectionProvider provider) => this.provider = provider;

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiDescriptionGroups.Items)
            {
                options.SwaggerDoc(description.GroupName, null);
            }
        }
    }
}

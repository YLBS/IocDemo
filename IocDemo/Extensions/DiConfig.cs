using System.Reflection;

namespace IocDemo.Extensions
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public static class DiConfig
    {
        /// <summary>
        /// 添加依赖注入，生命周期为Scoped
        /// </summary>
        /// <param name="services"></param>
        public static void AddDi(this IServiceCollection services)
        {
            // 获取当前正在执行的程序集
            //var currentAssembly = Assembly.GetExecutingAssembly();

            var serviceAssembly = Assembly.Load("Service");
            var iServiceAssembly = Assembly.Load("IService");

            var types = iServiceAssembly.GetTypes().Where(t => t.IsPublic);

            var types1 = serviceAssembly.GetTypes().Where(t => t.IsPublic);
            foreach (var type in types)
            {
                if (type.IsInterface)
                {
                    // 获取所有实现这个接口的类
                    var implementations = types1.Where(t => type.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();
                    if (implementations.Count == 1)
                    {
                        services.AddScoped(type, implementations[0]);
                    }
                }
            }
        }

    }
}

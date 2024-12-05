using Common.Tool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;

namespace IocDemo.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            #region 添加日志

            string requestPath = context.HttpContext.Request.Path;
            string method = context.HttpContext.Request.Method;
            string queryParameter = "参数：\n";
            if (method.ToLower() == "get")
            {
                var queryParameters = context.HttpContext.Request.Query;
                foreach (var param in queryParameters)
                {
                    queryParameter += $"{param.Key} = {string.Join(", ", param.Value)}\n";
                }
            }
            else if (method.ToLower() == "post")
            {
                try
                {
                    // FromForm 提交
                    var formCollection = context.HttpContext.Request.Form;
                    if (formCollection != null)
                    {
                        foreach (var param in formCollection)
                            queryParameter += $"{param.Key} = {string.Join(", ", param.Value)}\n";
                    }
                }
                catch (Exception)
                {

                }
                try
                {

                    // FromBody 提交
                    // 读取请求体内容
                    // 启用请求体缓冲
                    //context.HttpContext.Request.EnableBuffering();
                    //var buffer = new byte[context.HttpContext.Request.ContentLength.Value];
                    //context.HttpContext.Request.Body.Read(buffer, 0, buffer.Length);
                    //var bodyContent = Encoding.UTF8.GetString(buffer);
                    //queryParameter += bodyContent;
                    //// 重置请求体流的位置，以便后续操作可以再次读取它
                    //context.HttpContext.Request.Body.Position = 0;
                }
                catch (Exception e)
                {
                }

            }
            LogConfig.TestSetConfig(context.Exception.Message + "\n 请求API:" + requestPath + "\n " + queryParameter);

            #endregion

            if (context.Exception is BadHttpRequestException)
            {
                // 设置结果
                context.Result = new ObjectResult(ResultMode<string>.Failed(context.Exception.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                // 设置结果
                context.Result = new ObjectResult(ResultMode<string>.Failed(context.Exception.Message))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            // 标记异常已经处理，以避免后续的过滤器处理
            context.ExceptionHandled = true;
        }

    }
}

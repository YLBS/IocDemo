using Microsoft.AspNetCore.Mvc;
using Model;

namespace IocDemo.Controllers.Test2
{
    /// <summary>
    /// 测试2
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "分组2")]
    public class Test1Controller : ControllerBase
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetData(int i)
        {
            await Task.Delay(500);
            if (i > 5)
                return Ok(ResultMode<string>.Success());
            return Ok(ResultMode<string>.Failed());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DelDataById(int i)
        {
            await Task.Delay(500);
            if (i > 5)
                return Ok(ResultMode<string>.Success());
            return Ok(ResultMode<string>.Failed());
        }
    }
}

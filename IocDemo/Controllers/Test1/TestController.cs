using Common.Cache;
using IService;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace IocDemo.Controllers.Test1
{
    /// <summary>
    /// 测试1
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "分组1")]
    public class TestController : ControllerBase
    {
        private readonly IRoleService _roleService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        public TestController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Code 0 成功，1 失败</returns>
        [HttpPost]
        public async Task<IActionResult> AddData([FromForm] string name)
        {
            bool yn = await _roleService.AddRole(name);
            if (yn)
                return Ok(ResultMode<object>.Success());
            return Ok(ResultMode<object>.Failed());
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var list = await _roleService.GetData();
            return Ok(ResultMode<object>.Success(new { count = list.Count(),Data = list}));
        }

        /// <summary> 
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Code 0 成功，2 没找到</returns>
        [HttpGet]
        public async Task<IActionResult> GetDataById(int id)
        {
            var data= await _roleService.GetDataById(id);
            if (data == null)
                return Ok(ResultMode<object>.NotFound());
            return Ok(ResultMode<object>.Success(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Code 0 成功，1 失败</returns>
        [HttpDelete]
        public async Task<IActionResult> DelDataById(int id)
        {
            bool yn = await _roleService.DelRoleById(id);
            if(yn)
                return Ok(ResultMode<object>.Success());
            return Ok(ResultMode<object>.Failed());
        }
        /// <summary>
        /// 添加Redis缓存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRedis(string key, string value) {
            RedisCache.SetKey($"key_{key}", value, DateTime.Now.AddMinutes(1));
            return Ok(ResultMode<object>.Success());
        }
        /// <summary>
        /// 获取Redis指定键的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRedis(string key)
        {
            string value = RedisCache.GetKey($"key_{key}");
            return Ok(ResultMode<string>.Success(value));
        }
        /// <summary>
        /// 获取存储的所有的Redis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRedis()
        {
            var value = RedisCache.GetAll();
            return Ok(ResultMode<object>.Success(value));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelRedis(string key)
        {
            RedisCache.DelPosKey($"key_{key}");
            return Ok(ResultMode<string>.Success());
        }
    }
}

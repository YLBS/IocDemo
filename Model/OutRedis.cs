
namespace Model
{
    /// <summary>
    /// 输出Redis
    /// </summary>
    public class OutRedis
    {
        /// <summary>
        /// 构造方法赋值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="second"></param>
        public OutRedis(string redisKey, string redisValue, double second)
        {
            this.RedisKey = redisKey;
            RedisValue = redisValue;
            ValidTime = DateTime.Now.AddSeconds(second).ToString("G");
        }
        /// <summary>
        /// key
        /// </summary>
        public string RedisKey { get; set; }
        /// <summary>
        /// value
        /// </summary>
        public string RedisValue { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string ValidTime { get; set; }
    }
}

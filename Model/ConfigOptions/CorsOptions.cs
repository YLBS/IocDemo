using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfigOptions
{
    public class CorsOptions
    {
        public string Name { get; set; }
        /// <summary>
        /// 是否允许所有跨域
        /// </summary>
        public bool EnableAll { get; set; }
        /// <summary>
        /// 允许跨域的IP集合
        /// </summary>
        public List<Policy> Policy { get; set; }
    }

    public class Policy
    {
        public string Name { get; set; }
        /// <summary>
        /// Ip域名
        /// </summary>
        public string Domain { get; set; }
    }
}

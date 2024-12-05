using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class KeyValueModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

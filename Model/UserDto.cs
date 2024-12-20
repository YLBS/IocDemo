﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserDto
    {
        public string UserName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Entity.DemoDB;

public partial class UserInfo
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;
}

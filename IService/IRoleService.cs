using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IRoleService
    {
        Task<bool> AddRole(string name);
        Task<IEnumerable<KeyValueModel>> GetData();
        Task<KeyValueModel> GetDataById(int id);

        Task<bool> DelRoleById(int id);
    }
}

using Dapper;
using Entity.DemoDB;
using IService;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly DemoContext _context;
        public RoleService(DemoContext context) {
            _context = context;
        }
        public async Task<bool> AddRole(string name)
        {
            var param = new { name };
            string sql = $"insert into RoleInfo values(@name)";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, param);
            return result > 0;
            throw new NotImplementedException();
        }

        public async Task<bool> DelRoleById(int id)
        {
            string sql = $"delete  RoleInfo where id = {id}";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql);
            return result > 0;
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<KeyValueModel>> GetData()
        {
            string sql = "select Id,RoleName Name from RoleInfo";
            var result = await _context.Database.GetDbConnection().QueryAsync<KeyValueModel>(sql);
            return result;
        }

        public async Task<KeyValueModel> GetDataById(int id)
        {
            string sql = $"select Id,RoleName Name from RoleInfo where Id={id}";
            var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<KeyValueModel>(sql);
            if(result == null)
                throw new NotImplementedException($"{id}——不存在");
            return result;
        }
    }
}

using Model;
using System.IdentityModel.Tokens.Jwt;

namespace IocDemo.Authentication.JWT
{
    /// <summary>
    /// 生成验证token接口
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        Task<Token> IssueTokenAsync(LoginUserInfo loginUserInfo);
        /// <summary>
        /// 读取验证token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<JwtSecurityToken> ReadJwtToken(string token);
    }
}

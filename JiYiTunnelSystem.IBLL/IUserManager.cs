using System.Collections.Generic;
using System.Threading.Tasks;
using JiYiTunnelSystem.Dto;

namespace JiYiTunnelSystem.IBLL
{
    public interface IUserManager
    {
        Task Register(string name, string phone,string mail, string password);
        bool Login(string phone, string password, out long userId,out int authority);
        Task ChangePassword(long userId, string oldPwd, string newPwd);
        bool RetrivePassword(string name, string email);
        Task<UserDto> GetUserById(long id);
        Task<UserDto> GetUserByEmail(string email);
        Task<List<UserDto>> GetUsers(int pageIndex, int pageSize);
        Task<List<UserDto>> GetUsers(sbyte alarm,int pageIndex,int pageSize);
        Task<int> GetUserCount();
        Task RenamePwd(string email, string pwd);
        Task<int> GetUserCountByAlarm(sbyte alarm);
        Task ChangeUserIsAlarm(long id, bool alarm, long uid);
        Task ChangeUserAuth(long id, int role, long uid);
    }
}

using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class UserService:BaseService<users>,IUserService
    {
        public UserService():base(new JiYiContext())
        {

        }
    }
}

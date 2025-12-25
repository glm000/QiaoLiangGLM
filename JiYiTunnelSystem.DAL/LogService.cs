using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class LogService:BaseService<logs>,ILogService
    {
        public LogService():base(new JiYiContext())
        {

        }
    }
}

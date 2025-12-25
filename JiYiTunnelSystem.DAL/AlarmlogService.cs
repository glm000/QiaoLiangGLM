using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class AlarmlogService:BaseService<alarmlogs>,IAlarmlogService
    {
        public AlarmlogService():base(new JiYiContext())
        {

        }
    }
}

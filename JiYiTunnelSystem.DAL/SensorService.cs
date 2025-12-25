using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class SensorService:BaseService<sensors>,ISensorService
    {
        public SensorService():base(new JiYiContext())
        {

        }
    }
}

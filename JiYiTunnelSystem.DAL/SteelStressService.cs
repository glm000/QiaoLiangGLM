using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class SteelStressService:BaseService<steelstresses>,ISteelStressService
    {
        public SteelStressService():base(new JiYiContext())
        {

        }
    }
}

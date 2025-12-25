using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class StressService:BaseService<stresses>,IStressService
    {
        public StressService():base(new JiYiContext())
        {

        }
    }
}

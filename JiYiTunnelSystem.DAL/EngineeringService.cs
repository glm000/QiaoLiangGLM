using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class EngineeringService:BaseService<engineeringsites>,IEngineeringService
    {
        public EngineeringService() : base(new JiYiContext())
        {

        }
    }
}

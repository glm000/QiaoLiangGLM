using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class StrainService:BaseService<strains>,IStrainService
    {
        public StrainService():base(new JiYiContext())
        {

        }
    }
}

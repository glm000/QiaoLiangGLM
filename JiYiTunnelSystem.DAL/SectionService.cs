using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class SectionService:BaseService<sections>,ISectionService
    {
        public SectionService():base(new JiYiContext())
        {

        }
    }
}

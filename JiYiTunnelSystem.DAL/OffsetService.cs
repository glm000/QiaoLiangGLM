using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class OffsetService:BaseService<offsets>,IOffsetService
    {
        public OffsetService():base(new JiYiContext())
        {

        }
    }
}

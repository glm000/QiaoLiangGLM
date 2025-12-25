using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class SetValueLogService:BaseService<setvaluelogs>,ISetValueLogService
    {
        public SetValueLogService():base(new JiYiContext())
        {

        }
    }
}

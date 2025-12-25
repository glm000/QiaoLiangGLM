using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class MessageService:BaseService<messages>,IMessageService
    {
        public MessageService():base(new JiYiContext())
        {

        }
    }
}

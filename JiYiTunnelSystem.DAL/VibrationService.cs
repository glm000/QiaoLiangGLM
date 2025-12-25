using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.DAL
{
    public class VibrationService:BaseService<vibrations>,IVibrationService
    {
        public VibrationService():base(new JiYiContext())
        {

        }
    }
}

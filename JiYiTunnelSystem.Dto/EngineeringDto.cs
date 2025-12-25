using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiYiTunnelSystem.Dto
{
    public class EngineeringDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal? StrainAlarmValue { get; set; }

        public decimal? StrainContorlValue { get; set; }

        public decimal? OffsetAlarmValue { get; set; }

        public decimal? OffsetControlValue { get; set; }

        public decimal? VibrationAlarmValue { get; set; }

        public decimal? VibrationControlValue { get; set; }

        public decimal? SteelStressAlarmValue { get; set; }
    }
}

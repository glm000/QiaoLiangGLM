using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class AlarmSettingDto
    {
        [Display(Name ="应变预警")]
        public decimal? StrainAlarm { get; set; }
        [Display(Name ="应变控制")]
        public decimal? StrainControl { get; set; }
        [Display(Name ="位移预警")]
        public decimal? OffsetAlarm { get; set; }
        [Display(Name ="位移控制")]
        public decimal? OffsetControl { get; set; }
        [Display(Name ="应急封堵墙振动预警")]
        public decimal? VibrationAlarm_YJ { get; set; }
        [Display(Name ="正洞与斜井振动预警")]
        public decimal? VibrationAlarm_ZD { get; set; }
        [Display(Name ="斜井与连接通道振动预警")]
        public decimal? VibrationAlarm_LJ { get; set; }
        [Display(Name ="压力预警")]
        public decimal? StressAlarm { get; set; }
    }
}

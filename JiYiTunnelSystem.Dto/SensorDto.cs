using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class SensorDto
    {
        public long Id { get; set; }

        public long SectionId { get; set; }
        [Display(Name ="节点")]
        public string SensorNum { get; set; }
        [Display(Name ="截面")]
        public string SectionNum { get; set; }
        public long EngId { get; set; }
        [Display(Name ="部位")]
        public string EngName { get; set; }
        [Display(Name ="竖井")]
        public string Shaft { get; set; }
        [Display(Name ="类型")]
        public string SensorType { get; set; }
        [Display(Name ="状态")]
        public bool State { get; set; }
        [Display(Name ="备注")]
        public string Comment { get; set; }
        [Display(Name ="初始值")]
        public decimal? InitialValue { get; set; }

        [Display(Name ="数据")]
        public decimal? LatestData { get; set; }
        [Display(Name ="最新监测时间")]
        public DateTime? LatestTime { get; set; }
        public decimal? K { get; set; }

        public sbyte IsAlarm { get; set; }
    }
}

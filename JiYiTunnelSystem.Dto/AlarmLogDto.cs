using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class AlarmLogDto
    {
        public long Id { get; set; }
        [Display(Name ="竖井")]
        public string Shaft { get; set; }
        public long EngId { get; set; }
        [Display(Name ="部位")]
        public string EngName { get; set; }
        public long SectionId { get; set; }
        [Display(Name ="截面")]
        public string SectionNum { get; set; }
        public long SensorId { get; set; }
        [Display(Name ="节点")]
        public string SensorNum { get; set; }
        [Display(Name ="因素")]
        public string Type { get; set; }
        [Display(Name ="测量值")]
        public decimal? Data { get; set; }
        [Display(Name ="等级")]
        public sbyte Grade { get; set; }
        [Display(Name ="时间")]
        public DateTime? CreateTime { get; set; }
    }
}

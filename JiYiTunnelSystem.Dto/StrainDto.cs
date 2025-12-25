using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class StrainDto
    {
        public long Id { get; set; }

        [Display(Name = "竖井")]
        public string Shaft { get; set; }
        public long EngId { get; set; }
        [Display(Name = "部位")]
        public string EngName { get; set; }
        public long SectionId { get; set; }
        [Display(Name = "截面")]
        public string SectionNum { get; set; }

        public decimal? Data1 { get; set; }

        public decimal? Data2 { get; set; }

        public decimal? Data3 { get; set; }

        public decimal? Data4 { get; set; }

        public decimal? Data5 { get; set; }

        public decimal? Data6 { get; set; }

        public decimal? Data7 { get; set; }

        public decimal? Data8 { get; set; }
        public decimal? Data9 { get; set; }

        public string SensorNum1 { get; set; }

        public string SensorNum2 { get; set; }

        public string SensorNum3 { get; set; }

        public string SensorNum4 { get; set; }

        public string SensorNum5 { get; set; }
        public string SensorNum6 { get; set; }
        public string SensorNum7 { get; set; }
        public string SensorNum8 { get; set; }
        public string SensorNum9 { get; set; }

        [Display(Name = "时间"), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime? CreateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.MVCSite.Models.Home
{
    public class CreateSensorVM
    {
        public long Id { get; set; }
        [Display(Name ="竖井")]
        public string Shaft { get; set; }
        [Display(Name ="部位")]
        public long EngId { get; set; }
        [Display(Name ="截面")]
        public long SectionId { get; set; }
        [Display(Name ="类型")]
        public string SensorType { get; set; }
        [Display(Name = "编号"),Required]
        public string SensorNum { get; set; }
        [Display(Name ="初始值")]
        public string InitialValue { get; set; }
        [Display(Name ="备注")]
        public string Comment { get; set; }
    }
}
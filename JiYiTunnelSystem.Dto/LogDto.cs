using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class LogDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        [Display(Name ="姓名")]
        public string UserName { get; set; }
        [Display(Name ="手机")]
        public string Phone { get; set; }
        [Display(Name ="记录")]
        public string Behavior { get; set; }
        [Display(Name ="时间")]
        public DateTime? CreateTime { get; set; } 
    }
}

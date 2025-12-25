using System;
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.Dto
{
    public class UserDto
    {
        public long Id { get; set; }

        [Display(Name ="姓名")]
        public string Name { get; set; }

        [Display(Name ="手机")]
        public string Phone { get; set; }

        [Display(Name ="邮箱")]
        public string Email { get; set; }
        [Display(Name="密码")]
        public string Password { get; set; }
        [Display(Name="角色")]
        public int Authority { get; set; }
        [Display(Name ="报警联系人")]
        public sbyte IsAlarm { get; set; }

        [Display(Name ="注册时间")]
        public DateTime? CreateTime { get; set; }
    }
}

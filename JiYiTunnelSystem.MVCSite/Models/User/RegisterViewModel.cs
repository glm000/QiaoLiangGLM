using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.MVCSite.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required, StringLength(11, MinimumLength = 3)]
        [RegularExpression(@"1\d{10}")]
        [Display(Name = "手机")]
        public string Phone { get; set; }

        [Required,DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*\.[a-z]{2,}")]
        [Display(Name="邮箱")]
        public string Email { get; set; }

        [Required, StringLength(10,MinimumLength =6)]
        [RegularExpression("(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z\\W]{6,10}")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "确认密码")]
        public string ConfirmPwd { get; set; }

        [Required, StringLength(4, MinimumLength = 4)]
        [Display(Name="验证码")]
        public string VerifyCode { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.MVCSite.Models.User
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(@"1\d{10}")]
        [Display(Name = "手机")]
        public string Phone { get; set; }
        [Required, DataType(DataType.Password)]
        [RegularExpression(@"(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z\\W]{6,10}")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "记住密码")]
        public bool RemeberMe { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace JiYiTunnelSystem.MVCSite.Models.User
{
    public class ChangePasswordVM
    {
        [Required, StringLength(10,MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string OldPwd { get; set; }

        [Required, StringLength(10,MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPwd { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare(nameof(NewPwd))]
        [Display(Name = "确认密码")]
        public string ConfirmPwd { get; set; }
    }
}
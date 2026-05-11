using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Accounts
{
    /// <summary>
    /// 重置密码输入参数
    /// </summary>
    public class ResetPasswordInput
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// 确认新密码
        /// </summary>
        [Required]
        [StringLength(100)]
        [Compare(nameof(NewPassword), ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

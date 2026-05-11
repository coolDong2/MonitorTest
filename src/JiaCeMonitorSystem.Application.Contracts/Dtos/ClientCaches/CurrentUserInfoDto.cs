using System;

namespace JiaCeMonitorSystem.Dtos.ClientCaches
{
    /// <summary>
    /// 当前用户信息数据传输对象
    /// </summary>
    public class CurrentUserInfoDto
    {
        public Guid Id { get; set; }
        public string Account { get; set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        public string? NickName { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? HeadIcon { get; set; }
        public string? Gender { get; set; }
        public string? GenderText { get; set; }
        public string? Birthday { get; set; }
        public string? BirthdayFormatted { get; set; }
        public int Age { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
        public string? WeChat { get; set; }
        public string? ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public int SecurityLevel { get; set; }
        public string? Signature { get; set; }
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? OrganizeId { get; set; }
        public string? OrganizeName { get; set; }
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? DutyId { get; set; }
        public string? DutyName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBoss { get; set; }
        public bool IsSenior { get; set; }
        public bool IsLeaderInDepts { get; set; }
        public string? UserTypeText { get; set; }
        public int SortCode { get; set; }
        public bool DeleteMark { get; set; }
        public bool EnabledMark { get; set; }
        public string? StatusText { get; set; }
        public string? Description { get; set; }
        public string? ErrorMsg { get; set; }
        public string? Remark { get; set; }
        public string? CreatorTime { get; set; }
        public string? CreatorTimeFormatted { get; set; }
        public string? CreatorUserId { get; set; }
        public string? CreatorUserName { get; set; }
        public string? LastModifyTime { get; set; }
        public string? LastModifyTimeFormatted { get; set; }
        public string? LastModifyUserId { get; set; }
        public string? LastModifyUserName { get; set; }
        public string? DeleteTime { get; set; }
        public string? DeleteTimeFormatted { get; set; }
        public string? DeleteUserId { get; set; }
        public string? DeleteUserName { get; set; }
        public string? DingTalkUserId { get; set; }
        public string? DingTalkUserName { get; set; }
        public string? DingTalkAvatar { get; set; }
        public string? WxOpenId { get; set; }
        public string? WxNickName { get; set; }
        public string? HeadImgUrl { get; set; }
        public int MsgCount { get; set; }
        public bool IsChecked { get; set; }
        public string? FullHeadIcon { get; set; }
    }
}

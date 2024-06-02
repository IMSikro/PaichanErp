// 麻省理工学院许可证
//
// 版权所有 (c) 2021-2023 zuohuaijun，大名科技（天津）有限公司  联系电话/微信：18020030720  QQ：515096995
//
// 特此免费授予获得本软件的任何人以处理本软件的权利，但须遵守以下条件：在所有副本或重要部分的软件中必须包括上述版权声明和本许可声明。
//
// 软件按“原样”提供，不提供任何形式的明示或暗示的保证，包括但不限于对适销性、适用性和非侵权的保证。
// 在任何情况下，作者或版权持有人均不对任何索赔、损害或其他责任负责，无论是因合同、侵权或其他方式引起的，与软件或其使用或其他交易有关。

namespace Admin.NET.Core.Service;


[ExcelImporter(IsLabelingError = true)]
public class UserDto
{
    /// <summary>
    /// 账号
    /// </summary>
    [ImporterHeader(Name = "账号")]
    [Required(ErrorMessage = "账号不能为空")]
    public virtual string Account { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [ImporterHeader(Name = "真实姓名")]
    [Required(ErrorMessage = "真实姓名不能为空")]
    public virtual string RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [ImporterHeader(Name = "昵称")]
    public string? NickName { get; set; }

    /// <summary>
    /// 性别-男_1、女_2
    /// </summary>
    [ImporterHeader(Name = "性别")]
    [Required(ErrorMessage = "性别不能为空")]
    public GenderEnum Sex { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    [ImporterHeader(Name = "年龄")]
    [Required(ErrorMessage = "年龄不能为空")]
    public int Age { get; set; }

    /// <summary>
    /// 工时单价
    /// </summary>
    [ImporterHeader(Name = "工时单价")]
    public double? UnitPrice { get; set; }

    /// <summary>
    /// 个人技能
    /// </summary>
    [ImporterHeader(Name = "个人技能")]
    public string? PersonalSkill { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [ImporterHeader(Name = "出生日期")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [ImporterHeader(Name = "民族")]
    public string? Nation { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [ImporterHeader(Name = "手机号码")]
    [Required(ErrorMessage = "手机号码不能为空")]
    public string? Phone { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    [ImporterHeader(Name = "证件类型")]
    public CardTypeEnum CardType { get; set; }

    /// <summary>
    /// 身份证号
    /// </summary>
    [ImporterHeader(Name = "身份证号")]
    public string? IdCardNum { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [ImporterHeader(Name = "邮箱")]
    public string? Email { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [ImporterHeader(Name = "地址")]
    public string? Address { get; set; }

    /// <summary>
    /// 文化程度
    /// </summary>
    [ImporterHeader(Name = "文化程度")]
    public CultureLevelEnum CultureLevel { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [ImporterHeader(Name = "政治面貌")]
    public string? PoliticalOutlook { get; set; }

    /// <summary>
    /// 毕业院校
    /// </summary>
    [ImporterHeader(Name = "毕业院校")]
    public string? College { get; set; }

    /// <summary>
    /// 办公电话
    /// </summary>
    [ImporterHeader(Name = "办公电话")]
    public string? OfficePhone { get; set; }

    /// <summary>
    /// 紧急联系人
    /// </summary>
    [ImporterHeader(Name = "紧急联系人")]
    public string? EmergencyContact { get; set; }

    /// <summary>
    /// 紧急联系人电话
    /// </summary>
    [ImporterHeader(Name = "紧急联系人电话")]
    public string? EmergencyPhone { get; set; }

    /// <summary>
    /// 紧急联系人地址
    /// </summary>
    [ImporterHeader(Name = "紧急联系人地址")]
    public string? EmergencyAddress { get; set; }

    /// <summary>
    /// 个人简介
    /// </summary>
    [ImporterHeader(Name = "个人简介")]
    public string? Introduction { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [ImporterHeader(Name = "备注")]
    public string? Remark { get; set; }

    /// <summary>
    /// 直属机构
    /// </summary>
    [ImporterHeader(Name = "直属机构")]
    public string OrgName { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    [ImporterHeader(Name = "职位")]
    public string PosName { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    [ImporterHeader(Name = "工号")]
    public string? JobNum { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    [ImporterHeader(Name = "入职日期")]
    public DateTime? JoinDate { get; set; }
}

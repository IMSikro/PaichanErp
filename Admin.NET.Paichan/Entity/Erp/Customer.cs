using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 客户
/// </summary>
[SugarTable("ERP_Customer", "客户")]
[IncreTable]
public class Customer  : EntityTenant
{
    
    /// <summary>
    /// 客户编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "CustomerCode", ColumnDescription = "客户编号", Length = 100)]
    public string CustomerCode { get; set; }
    
    /// <summary>
    /// 客户名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "CustomerName", ColumnDescription = "客户名称", Length = 100)]
    public string CustomerName { get; set; }

    /// <summary>
    /// 签约开始日期
    /// </summary>
    [SugarColumn(ColumnDescription = "签约开始日期")]
    public DateTime? ContractStartDate { get; set; }

    /// <summary>
    /// 签约结束日期
    /// </summary>
    [SugarColumn(ColumnDescription = "签约结束日期")]
    public DateTime? ContractEndDate { get; set; }

    /// <summary>
    /// 省份
    /// </summary>
    [SugarColumn(ColumnDescription = "省份", Length = 64)]
    public string? Province { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnDescription = "城市", Length = 64)]
    public string? City { get; set; }

    /// <summary>
    /// 区县
    /// </summary>
    [SugarColumn(ColumnDescription = "区县", Length = 64)]
    public string? Area { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    [SugarColumn(ColumnDescription = "详细地址", Length = 255)]
    public string? Address { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(ColumnDescription = "姓名", Length = 64)]
    public string? Surname { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    [SugarColumn(ColumnDescription = "职位", Length = 50)]
    public string? CustomerPos { get; set; }

    /// <summary>
    /// 固定电话
    /// </summary>
    [SugarColumn(ColumnDescription = "固定电话", Length = 20)]
    public string? TelPhone { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [SugarColumn(ColumnDescription = "手机号", Length = 20)]
    public string? Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnDescription = "邮箱", Length = 100)]
    public string? Email { get; set; }

    /// <summary>
    /// QQ
    /// </summary>
    [SugarColumn(ColumnDescription = "QQ", Length = 20)]
    public string? QQ { get; set; }

    /// <summary>
    /// 开户名称
    /// </summary>
    [SugarColumn(ColumnDescription = "开户名称", Length = 100)]
    public string? AccountName { get; set; }

    /// <summary>
    /// 开户银行
    /// </summary>
    [SugarColumn(ColumnDescription = "开户银行", Length = 100)]
    public string? AccountBank { get; set; }

    /// <summary>
    /// 银行账号
    /// </summary>
    [SugarColumn(ColumnDescription = "银行账号", Length = 100)]
    public string? BankNumber { get; set; }

    /// <summary>
    /// 发票抬头
    /// </summary>
    [SugarColumn(ColumnDescription = "发票抬头", Length = 100)]
    public string? InvoiceHeader { get; set; }

    /// <summary>
    /// 纳税人识别码
    /// </summary>
    [SugarColumn(ColumnDescription = "纳税人识别码", Length = 100)]
    public string? TaxpayerIdentification { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
    public int? Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

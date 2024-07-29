using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 供应商
/// </summary>
[SugarTable("ERP_Supplier", "供应商")]
[IncreTable]
public class Supplier  : EntityTenant
{
    
    /// <summary>
    /// 供应商编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "SupplierCode", ColumnDescription = "供应商编号", Length = 100)]
    public string SupplierCode { get; set; }
    
    /// <summary>
    /// 供应商名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "SupplierName", ColumnDescription = "供应商名称", Length = 100)]
    public string SupplierName { get; set; }

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
    public string? SupplierPos { get; set; }

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

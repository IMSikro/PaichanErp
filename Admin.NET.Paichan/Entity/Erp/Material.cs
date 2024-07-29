using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 物料
/// </summary>
[SugarTable("ERP_Material", "物料")]
[IncreTable]
public class Material  : EntityTenant
{

    /// <summary>
    /// 物料类型外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "MaterialTypeId", ColumnDescription = "物料类型外键")]
    public long MaterialTypeId { get; set; }

    /// <summary>
    /// 物料编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "MaterialCode", ColumnDescription = "物料编号", Length = 100)]
    public string MaterialCode { get; set; }

    /// <summary>
    /// 物料名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "MaterialName", ColumnDescription = "物料名称", Length = 100)]
    public string MaterialName { get; set; }

    /// <summary>
    /// 物料规格
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "MaterialNorm", ColumnDescription = "物料规格", Length = 100)]
    public string MaterialNorm { get; set; }

    /// <summary>
    /// 采购单价
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "CostPrice", ColumnDescription = "采购单价")]
    public double CostPrice { get; set; }

    /// <summary>
    /// 税率(%)
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DutyRate", ColumnDescription = "税率(%)")]
    public double DutyRate { get; set; }

    /// <summary>
    /// 税价合计
    /// </summary>
    public double CostDutyTotalPrice => CostPrice * ((DutyRate / 100) + 1);

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "SupplierId", ColumnDescription = "供应商")]
    public long? SupplierId { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(SupplierId))]
    public Supplier? Supplier { get; set; }

    /// <summary>
    /// 安全库存
    /// </summary>
    [SugarColumn(ColumnName = "SafetyStock", ColumnDescription = "安全库存")]
    public double SafetyStock { get; set; }

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

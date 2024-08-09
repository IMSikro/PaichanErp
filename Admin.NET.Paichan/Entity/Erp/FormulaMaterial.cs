using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 配方物料
/// </summary>
[SugarTable("ERP_ProduceFormulaMaterial", "配方物料")]
[IncreTable]
public class ProduceFormulaMaterial  : EntityTenant
{
    /// <summary>
    /// 配方
    /// </summary>
    [SugarColumn(ColumnName = "ProduceFormulaId", ColumnDescription = "配方")]
    public long? ProduceFormulaId { get; set; }

    /// <summary>
    /// 配方
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ProduceFormulaId))]
    public ProduceFormula? ProduceFormula { get; set; }

    /// <summary>
    /// 小料
    /// </summary>
    [SugarColumn(ColumnName = "MaterialId", ColumnDescription = "小料")]
    public long? MaterialId { get; set; }

    /// <summary>
    /// 小料
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(MaterialId))]
    public Material? Material { get; set; }

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

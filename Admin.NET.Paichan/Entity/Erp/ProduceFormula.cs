using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 配方
/// </summary>
[SugarTable("ERP_ProduceFormula", "配方")]
[IncreTable]
public class ProduceFormula  : EntityTenant
{
    /// <summary>
    /// 产品外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceId", ColumnDescription = "产品外键")]
    public long ProduceId { get; set; }

    /// <summary>
    /// 产品
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ProduceId))]
    public Produce Produce { get; set; }

    /// <summary>
    /// 配方编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceFormulaCode", ColumnDescription = "配方编号", Length = 100)]
    public string ProduceFormulaCode { get; set; }

    /// <summary>
    /// 配方名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceFormulaName", ColumnDescription = "配方名称", Length = 100)]
    public string ProduceFormulaName { get; set; }

    /// <summary>
    /// 配方版本
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "FormulaVersion", ColumnDescription = "配方版本", Length = 100)]
    public string FormulaVersion { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "IsEnable", ColumnDescription = "是否启用")]
    public bool IsEnable { get; set; }

    /// <summary>
    /// 配方成本
    /// </summary>
    [SugarColumn(ColumnName = "FormulaCosts", ColumnDescription = "配方成本")]
    public double FormulaCosts { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

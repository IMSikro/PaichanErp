using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 物料类型
/// </summary>
[SugarTable("ERP_MaterialType", "物料类型")]
[IncreTable]
public class MaterialType  : EntityTenant
{
    
    /// <summary>
    /// 物料类型
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "MaterialTypeName", ColumnDescription = "物料类型", Length = 100)]
    public string MaterialTypeName { get; set; }

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

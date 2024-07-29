using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 仓库
/// </summary>
[SugarTable("ERP_Store", "仓库")]
[IncreTable]
public class Store  : EntityTenant
{
    
    /// <summary>
    /// 仓库编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "StoreCode", ColumnDescription = "仓库编号", Length = 100)]
    public string StoreCode { get; set; }
    
    /// <summary>
    /// 仓库名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "StoreName", ColumnDescription = "仓库名称", Length = 100)]
    public string StoreName { get; set; }

    /// <summary>
    /// 库位
    /// </summary>
    [SugarColumn(ColumnName = "StoreLocation", ColumnDescription = "库位", Length = 255)]
    public string? StoreLocation { get; set; }

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

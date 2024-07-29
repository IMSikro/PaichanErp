using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 库位
/// </summary>
[SugarTable("ERP_StoreLocation", "库位")]
[IncreTable]
public class StoreLocation : EntityTenant
{

    /// <summary>
    /// 库位编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "StoreLocationCode", ColumnDescription = "库位编号", Length = 100)]
    public string StoreLocationCode { get; set; }

    /// <summary>
    /// 库位名称
    /// </summary>
    [SugarColumn(ColumnName = "StoreLocationName", ColumnDescription = "库位名称", Length = 100)]
    public string? StoreLocationName { get; set; }

    /// <summary>
    /// 仓库外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "StoreId", ColumnDescription = "仓库外键")]
    public long StoreId { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(StoreId))]
    public Store Store { get; set; }

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

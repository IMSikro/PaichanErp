using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 库存盘点
/// </summary>
[SugarTable("ERP_StockTake", "库存盘点")]
[IncreTable]
public class StockTake : EntityTenant
{
    /// <summary>
    /// 盘点单号
    /// </summary>
    [SugarColumn(ColumnName = "StockTakeCode", ColumnDescription = "盘点单号", Length = 50)]
    public string? StockTakeCode { get; set; }

    /// <summary>
    /// 盘点日期
    /// </summary>
    [SugarColumn(ColumnName = "StockTakeDate", ColumnDescription = "盘点日期")]
    public DateTime? StockTakeDate { get; set; }

    /// <summary>
    /// 盘点数量
    /// </summary>
    [SugarColumn(ColumnName = "StockTakeNumber", ColumnDescription = "盘点数量")]
    public double? StockTakeNumber { get; set; }

    /// <summary>
    /// 盈亏数量
    /// </summary>
    [SugarColumn(ColumnName = "StockTakeDiffNumber", ColumnDescription = "盈亏数量")]
    public double? StockTakeDiffNumber { get; set; }

    /// <summary>
    /// 产品外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceId", ColumnDescription = "产品外键")]
    public long ProduceId { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [SugarColumn(ColumnName = "ProduceCode", ColumnDescription = "产品编号", Length = 50)]
    public string? ProduceCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceName", ColumnDescription = "产品名称", Length = 50)]
    public string ProduceName { get; set; }

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
    /// 库位
    /// </summary>
    [SugarColumn(ColumnName = "StoreLocation", ColumnDescription = "库位", Length = 255)]
    public string? StoreLocation { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

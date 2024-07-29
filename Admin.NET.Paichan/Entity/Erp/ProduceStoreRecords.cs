using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 生产入库单
/// </summary>
[SugarTable("ERP_ProduceStoreRecords", "生产入库单")]
[IncreTable]
public class ProduceStoreRecords : EntityTenant
{

    /// <summary>
    /// 订单外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "订单外键")]
    public long OrderId { get; set; }

    /// <summary>
    /// 订单
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(OrderId))]
    public Order Order { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "BatchNumber", ColumnDescription = "批次号", Length = 50)]
    public string BatchNumber { get; set; }

    /// <summary>
    /// 批次总量
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Quantity", ColumnDescription = "批次总量")]
    public double Quantity { get; set; }

    /// <summary>
    /// 生产总量
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "RealQuantity", ColumnDescription = "生产总量")]
    public double RealQuantity { get; set; }

    /// <summary>
    /// 批数
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "BatchCount", ColumnDescription = "批数")]
    public int BatchCount { get; set; }

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
    /// 入库产值
    /// </summary>
    [SugarColumn(ColumnName = "TotalPrice", ColumnDescription = "入库产值")]
    public double? TotalPrice { get; set; }

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "InStoreDate", ColumnDescription = "入库日期")]
    public DateTime? InStoreDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

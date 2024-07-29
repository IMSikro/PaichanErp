using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 库存调拨
/// </summary>
[SugarTable("ERP_StoreDeploy", "库存调拨")]
[IncreTable]
public class StoreDeploy : EntityTenant
{
    /// <summary>
    /// 调拨单号
    /// </summary>
    [SugarColumn(ColumnName = "StoreDeployCode", ColumnDescription = "调拨单号", Length = 50)]
    public string? StoreDeployCode { get; set; }

    /// <summary>
    /// 调拨日期
    /// </summary>
    [SugarColumn(ColumnName = "StoreDeployDate", ColumnDescription = "调拨日期")]
    public DateTime? StoreDeployDate { get; set; }

    /// <summary>
    /// 调拨数量
    /// </summary>
    [SugarColumn(ColumnName = "StoreDeployNumber", ColumnDescription = "调拨数量")]
    public double? StoreDeployNumber { get; set; }

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
    /// 调出仓库外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OutStoreId", ColumnDescription = "调出仓库外键")]
    public long OutStoreId { get; set; }

    /// <summary>
    /// 调出仓库
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(OutStoreId))]
    public Store OutStore { get; set; }

    /// <summary>
    /// 调出仓库编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OutStoreCode", ColumnDescription = "调出仓库编号", Length = 100)]
    public string OutStoreCode { get; set; }

    /// <summary>
    /// 调出仓库名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OutStoreName", ColumnDescription = "调出仓库名称", Length = 100)]
    public string OutStoreName { get; set; }

    /// <summary>
    /// 调出仓库库位
    /// </summary>
    [SugarColumn(ColumnName = "OutStoreLocation", ColumnDescription = "调出仓库库位", Length = 255)]
    public string? OutStoreLocation { get; set; }

    /// <summary>
    /// 调入仓库外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "InStoreId", ColumnDescription = "调入仓库外键")]
    public long InStoreId { get; set; }

    /// <summary>
    /// 调入仓库
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(InStoreId))]
    public Store InStore { get; set; }

    /// <summary>
    /// 调入仓库编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "InStoreCode", ColumnDescription = "调入仓库编号", Length = 100)]
    public string InStoreCode { get; set; }

    /// <summary>
    /// 调入仓库名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "InStoreName", ColumnDescription = "调入仓库名称", Length = 100)]
    public string InStoreName { get; set; }

    /// <summary>
    /// 调入仓库库位
    /// </summary>
    [SugarColumn(ColumnName = "InStoreLocation", ColumnDescription = "调入仓库库位", Length = 255)]
    public string? InStoreLocation { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

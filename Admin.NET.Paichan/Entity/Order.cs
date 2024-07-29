using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 生产订单
/// </summary>
[SugarTable("PC_Order", "生产订单")]
[IncreTable]
public class Order : EntityTenant
{

    /// <summary>
    /// 订单编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OrderCode", ColumnDescription = "订单编号", Length = 100)]
    public string OrderCode { get; set; }
    
    /// <summary>
    /// 下单日期
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OrderDate", ColumnDescription = "下单日期")]
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// 交货日期
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DeliveryDate", ColumnDescription = "交货日期")]
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// 实际开工时间
    /// </summary>
    [SugarColumn(ColumnName = "StartDate", ColumnDescription = "实际开工时间")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 实际完成时间
    /// </summary>
    [SugarColumn(ColumnName = "EndDate", ColumnDescription = "实际完成时间")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否完工
    /// </summary>
    [SugarColumn(ColumnName = "IsEnd", ColumnDescription = "是否完工", DefaultValue = "0")]
    public bool IsEnd { get; set; } = false;

    /// <summary>
    /// 产品外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceId", ColumnDescription = "产品外键")]
    public long ProduceId { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [SugarColumn(ColumnName = "ProduceCode", ColumnDescription = "产品编号",Length = 100)]
    public string? ProduceCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceName", ColumnDescription = "产品名称",Length = 100)]
    public string ProduceName { get; set; }

    /// <summary>
    /// 完工工艺
    /// </summary>
    [SugarColumn(ColumnName = "EndDeviceTypes", ColumnDescription = "完工工艺")]
    public string? EndDeviceTypes { get; set; }

    /// <summary>
    /// 异常工艺
    /// </summary>
    [SugarColumn(ColumnName = "ErrorDeviceType", ColumnDescription = "异常工艺")]
    public long? ErrorDeviceType { get; set; }

    /// <summary>
    /// 设备类型(工艺)
    /// </summary>
    [SugarColumn(ColumnName = "DeviceTypes", ColumnDescription = "设备类型(工艺)", Length = 255)]
    public string? DeviceTypes { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "BatchNumber", ColumnDescription = "批次号",Length = 50)]
    public string BatchNumber { get; set; }

    /// <summary>
    /// 批次总量
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Quantity", ColumnDescription = "批次总量")]
    public double Quantity { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "pUnit", ColumnDescription = "计量单位",Length = 20)]
    public string pUnit { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Customer", ColumnDescription = "客户",Length = 50)]
    public string Customer { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

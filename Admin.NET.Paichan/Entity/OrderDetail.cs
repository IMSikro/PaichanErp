using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 生产订单
/// </summary>
[SugarTable("PC_OrderDetail", "生产订单排班")]
[IncreTable]
public class OrderDetail : EntityTenant
{

    /// <summary>
    /// 订单外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "订单外键")]
    public long OrderId { get; set; }

    /// <summary>
    /// 班次序号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OrderDetailCode", ColumnDescription = "班次序号")]
    public string OrderDetailCode { get; set; }

    /// <summary>
    /// 班次产量
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Qty", ColumnDescription = "班次产量")]
    public double? Qty { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "pUnit", ColumnDescription = "计量单位")]
    public string? pUnit { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
    public int? Sort { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "StartDate", ColumnDescription = "开始时间")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "EndDate", ColumnDescription = "结束时间")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 设备外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DeviceId", ColumnDescription = "设备外键")]
    public long DeviceId { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "OperatorUsers", ColumnDescription = "操作人员", Length = 255)]
    public string OperatorUsers { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

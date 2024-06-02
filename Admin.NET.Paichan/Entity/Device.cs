using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 设备
/// </summary>
[SugarTable("PC_Device","设备")]
[IncreTable]
public class Device  : EntityTenant
{
    /// <summary>
    /// 设备类型外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DeviceTypeId", ColumnDescription = "设备类型外键")]
    public long DeviceTypeId { get; set; }
    
    /// <summary>
    /// 设备编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DeviceCode", ColumnDescription = "设备编号", Length = 100)]
    public string DeviceCode { get; set; }
    
    /// <summary>
    /// 设备名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "DeviceName", ColumnDescription = "设备名称", Length = 100)]
    public string DeviceName { get; set; }
    
    /// <summary>
    /// 设备系数
    /// </summary>
    [SugarColumn(ColumnName = "DeviceCoefficient", ColumnDescription = "设备系数", Length = 255)]
    public string? DeviceCoefficient { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "OperatorUsers", ColumnDescription = "操作人员", Length = 255)]
    public string? OperatorUsers { get; set; }

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

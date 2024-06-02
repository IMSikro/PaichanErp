using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 产品
/// </summary>
[SugarTable("PC_Produce","产品")]
[IncreTable]
public class Produce  : EntityTenant
{
    /// <summary>
    /// 产品类型外键
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceType", ColumnDescription = "产品类型外键")]
    public long ProduceType { get; set; }
    
    /// <summary>
    /// 产品编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceCode", ColumnDescription = "产品编号", Length = 100)]
    public string ProduceCode { get; set; }
    
    /// <summary>
    /// 产品名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProduceName", ColumnDescription = "产品名称", Length = 100)]
    public string ProduceName { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
    /// <summary>
    /// 产品LAB颜色
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ColorLab", ColumnDescription = "产品LAB颜色", Length = 100)]
    public string ColorLab { get; set; }
    
    /// <summary>
    /// 产品RGB颜色
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ColorRgb", ColumnDescription = "产品RGB颜色", Length = 100)]
    public string ColorRgb { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "pUnit", ColumnDescription = "计量单位")]
    public string? pUnit { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [SugarColumn(ColumnName = "UnitId", ColumnDescription = "计量单位")]
    public long? UnitId { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UnitId))]
    public SystemUnit? SystemUnit { get; set; }

    /// <summary>
    /// 产品系数
    /// </summary>
    [SugarColumn(ColumnName = "ProduceCoefficient", ColumnDescription = "产品系数", Length = 255)]
    public string? ProduceCoefficient { get; set; }
    
    /// <summary>
    /// 产品系列
    /// </summary>
    [SugarColumn(ColumnName = "ProduceSeries", ColumnDescription = "产品系列", Length = 255)]
    public string? ProduceSeries { get; set; }
    
    /// <summary>
    /// 设备类型(工艺)
    /// </summary>
    [SugarColumn(ColumnName = "DeviceTypes", ColumnDescription = "设备类型(工艺)", Length = 255)]
    public string? DeviceTypes { get; set; }
    
}

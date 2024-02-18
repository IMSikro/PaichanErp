using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 产品
/// </summary>
[SugarTable("PC_Produce","产品")]
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
    /// 是否搅拌
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "IsMix", ColumnDescription = "是否搅拌")]
    public bool IsMix { get; set; }

    /// <summary>
    /// 是否挤出
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "IsExtrusion", ColumnDescription = "是否挤出")]
    public bool IsExtrusion { get; set; }

    /// <summary>
    /// 是否破碎
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "IsMill", ColumnDescription = "是否破碎")]
    public bool IsMill { get; set; }

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
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

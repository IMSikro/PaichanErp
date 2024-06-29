using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 工艺设备
/// </summary>
[SugarTable("PC_DeviceType", "工艺设备")]
[IncreTable]
public class DeviceType  : EntityTenant
{
    /// <summary>
    /// 工艺名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "TypeName", ColumnDescription = "工艺名称", Length = 50)]
    public string TypeName { get; set; }

    /// <summary>
    /// 是否默认工艺
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "NormalType", ColumnDescription = "是否默认工艺",DefaultValue = "0")]
    public bool NormalType { get; set; } = false;

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

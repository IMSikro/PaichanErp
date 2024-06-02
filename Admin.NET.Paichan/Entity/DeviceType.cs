using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 设备类型
/// </summary>
[SugarTable("PC_DeviceType","设备类型")]
[IncreTable]
public class DeviceType  : EntityTenant
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "TypeName", ColumnDescription = "设备类型", Length = 50)]
    public string TypeName { get; set; }

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

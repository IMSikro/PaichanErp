using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 计量单位
/// </summary>
[SugarTable("PC_SystemUnit", "计量单位")]
[IncreTable]
public class SystemUnit  : EntityTenant
{
    /// <summary>
    /// 计量单位
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "UnitName", ColumnDescription = "计量单位", Length = 100)]
    public string UnitName { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

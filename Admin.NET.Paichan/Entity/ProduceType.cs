using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 产品类型
/// </summary>
[SugarTable("PC_ProduceType","产品类型")]
public class ProduceType  : EntityTenant
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [SugarColumn(ColumnName = "TypeName", ColumnDescription = "设备类型", Length = 50)]
    public string? TypeName { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

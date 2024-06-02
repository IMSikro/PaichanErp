using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 产品类型
/// </summary>
[SugarTable("PC_ProduceType","产品类型")]
[IncreTable]
public class ProduceType  : EntityTenant
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "TypeName", ColumnDescription = "产品类型", Length = 50)]
    public string? TypeName { get; set; }

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

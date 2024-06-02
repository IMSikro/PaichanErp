using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 设备分组
/// </summary>
[SugarTable("PC_DeviceGroup","设备分组")]
[IncreTable]
public class DeviceGroup  : EntityTenant
{
    /// <summary>
    /// 分组编号
    /// </summary>
    [SugarColumn(ColumnName = "GroupCode", ColumnDescription = "分组编号")]
    public long? GroupCode { get; set; }
    /// <summary>
    /// 分组名称
    /// </summary>
    [SugarColumn(ColumnName = "GroupName", ColumnDescription = "分组名称", Length = 50)]
    public string? GroupName { get; set; }
    
    /// <summary>
    /// 工艺设备Id
    /// </summary>
    [SugarColumn(ColumnName = "DeviceTypeIds", ColumnDescription = "工艺设备Id", Length = int.MaxValue)]
    public string? DeviceTypeIds { get; set; }
    
    /// <summary>
    /// 设备Id
    /// </summary>
    [SugarColumn(ColumnName = "DeviceIds", ColumnDescription = "设备Id", Length = int.MaxValue)]
    public string? DeviceIds { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
    
}

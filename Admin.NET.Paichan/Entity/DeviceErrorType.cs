using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 非生产类型
/// </summary>
[SugarTable("PC_DeviceErrorType","非生产类型")]
[IncreTable]
public class DeviceErrorType  : EntityTenant
{
    /// <summary>
    /// 非生产时间类型名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ErrorTypeName", ColumnDescription = "非生产时间类型名称", Length = 100)]
    public string ErrorTypeName { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

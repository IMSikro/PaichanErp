using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 工艺项目
/// </summary>
[SugarTable("ERP_ProcessProject", "工艺项目")]
[IncreTable]
public class ProcessProject  : EntityTenant
{
    /// <summary>
    /// 工艺项目编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProcessProjectCode", ColumnDescription = "工艺项目编号", Length = 100)]
    public string ProcessProjectCode { get; set; }

    /// <summary>
    /// 工艺项目名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProcessProjectName", ColumnDescription = "工艺项目名称", Length = 100)]
    public string ProcessProjectName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

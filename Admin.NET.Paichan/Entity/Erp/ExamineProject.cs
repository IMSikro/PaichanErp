using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 检验项目
/// </summary>
[SugarTable("ERP_ExamineProject", "检验项目")]
[IncreTable]
public class ExamineProject  : EntityTenant
{
    /// <summary>
    /// 检验项目编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ExamineProjectCode", ColumnDescription = "检验项目编号", Length = 100)]
    public string ExamineProjectCode { get; set; }

    /// <summary>
    /// 检验项目名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ExamineProjectName", ColumnDescription = "检验项目名称", Length = 100)]
    public string ExamineProjectName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

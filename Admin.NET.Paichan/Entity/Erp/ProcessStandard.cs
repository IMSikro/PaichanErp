using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 工艺标准
/// </summary>
[SugarTable("ERP_ProcessStandard", "工艺标准")]
[IncreTable]
public class ProcessStandard  : EntityTenant
{

    /// <summary>
    /// 配方
    /// </summary>
    [SugarColumn(ColumnName = "ProduceFormulaId", ColumnDescription = "配方")]
    public long? ProduceFormulaId { get; set; }

    /// <summary>
    /// 配方
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ProduceFormulaId))]
    public ProduceFormula? ProduceFormula { get; set; }

    /// <summary>
    /// 工艺标准编号
    /// </summary>
    [SugarColumn(ColumnName = "ProcessStandardCode", ColumnDescription = "工艺标准编号", Length = 100)]
    public string ProcessStandardCode { get; set; }

    /// <summary>
    /// 工艺标准名称
    /// </summary>
    [SugarColumn(ColumnName = "ProcessStandardName", ColumnDescription = "工艺标准名称", Length = 100)]
    public string ProcessStandardName { get; set; }

    /// <summary>
    /// 产品外键
    /// </summary>
    [SugarColumn(ColumnName = "ProduceId", ColumnDescription = "产品")]
    public long? ProduceId { get; set; }

    /// <summary>
    /// 产品
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ProduceId))]
    public Produce? Produce { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [SugarColumn(ColumnName = "ProduceCode", ColumnDescription = "产品编号", Length = 50)]
    public string? ProduceCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [SugarColumn(ColumnName = "ProduceName", ColumnDescription = "产品名称", Length = 50)]
    public string ProduceName { get; set; }

    /// <summary>
    /// 工艺设备外键
    /// </summary>
    [SugarColumn(ColumnName = "DeviceTypeId", ColumnDescription = "工艺设备")]
    public long? DeviceTypeId { get; set; }

    /// <summary>
    /// 工艺设备
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(DeviceTypeId))]
    public DeviceType? DeviceType { get; set; }

    /// <summary>
    /// 工艺设备名称
    /// </summary>
    [SugarColumn(ColumnName = "DeviceTypeName", ColumnDescription = "工艺设备名称", Length = 50)]
    public string DeviceTypeName { get; set; }

    /// <summary>
    /// 工艺项目外键
    /// </summary>
    [SugarColumn(ColumnName = "ProcessProjectId", ColumnDescription = "工艺项目")]
    public long? ProcessProjectId { get; set; }

    /// <summary>
    /// 工艺项目
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ProcessProjectId))]
    public ProcessProject? ProcessProject { get; set; }

    /// <summary>
    /// 工艺项目编号
    /// </summary>
    [SugarColumn(ColumnName = "ProcessProjectCode", ColumnDescription = "工艺项目编号", Length = 50)]
    public string? ProcessProjectCode { get; set; }

    /// <summary>
    /// 工艺项目名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "ProcessProjectName", ColumnDescription = "工艺项目名称", Length = 50)]
    public string ProcessProjectName { get; set; }

    /// <summary>
    /// 标准值
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "StandardValue", ColumnDescription = "标准值")]
    public double StandardValue { get; set; }

    /// <summary>
    /// 正公差
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Tolerance1", ColumnDescription = "正公差")]
    public double Tolerance1 { get; set; }

    /// <summary>
    /// 负公差
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Tolerance2", ColumnDescription = "负公差")]
    public double Tolerance2 { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注", Length = 255)]
    public string? Remark { get; set; }
}

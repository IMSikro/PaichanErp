using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Paichan;

/// <summary>
/// 设备列表输出参数
/// </summary>
[ExcelImporter(IsLabelingError = true)]
public class DeviceDto
{
    /// <summary>
    /// 工艺设备
    /// </summary>
    [ImporterHeader(Name = "工艺设备")]
    [Required(ErrorMessage = "工艺设备不能为空")]
    public string DeviceTypeName { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    [ImporterHeader(Name = "设备编号")]
    [Required(ErrorMessage = "设备编号不能为空")]
    public string DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    [ImporterHeader(Name = "设备名称")]
    [Required(ErrorMessage = "设备名称不能为空")]
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备系数
    /// </summary>
    [ImporterHeader(Name = "设备系数")]
    public string? DeviceCoefficient { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [ImporterHeader(Name = "备注")]
    public string? Remark { get; set; }

}

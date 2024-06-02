using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Paichan;

/// <summary>
/// 产品列表输出参数
/// </summary>
[ExcelImporter(IsLabelingError = true)]
public class ProduceDto
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [ImporterHeader(Name = "产品类型")]
    [Required(ErrorMessage = "产品类型不能为空")]
    public string? ProduceTypeName { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [ImporterHeader(Name = "产品编号")]
    [Required(ErrorMessage = "产品编号不能为空")]
    public string ProduceCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [ImporterHeader(Name = "产品名称")]
    [Required(ErrorMessage = "产品名称不能为空")]
    public string ProduceName { get; set; }

    /// <summary>
    /// 产品LAB颜色
    /// </summary>
    [ImporterHeader(Name = "产品LAB颜色")]
    [Required(ErrorMessage = "产品LAB颜色不能为空")]
    public string ColorLab { get; set; }

    /// <summary>
    /// 产品系数
    /// </summary>
    [ImporterHeader(Name = "产品系数")]
    public string? ProduceCoefficient { get; set; }

    ///// <summary>
    ///// 产品系列
    ///// </summary>
    //[ImporterHeader(Name = "产品系列")]
    //public string? ProduceSeries { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [ImporterHeader(Name = "计量单位")]
    public string? pUnit { get; set; }

    /// <summary>
    /// 设备类型(工艺)
    /// </summary>
    [ImporterHeader(Name = "工艺列表")]
    [Required(ErrorMessage = "工艺列表不能为空")]
    public string? DeviceTypes { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [ImporterHeader(Name = "备注")]
    public string? Remark { get; set; }

}

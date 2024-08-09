using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 库位管理输出参数
/// </summary>

[ExcelImporter(IsLabelingError = true)]
public class StoreLocationDto
{
    /// <summary>
    /// 库位
    /// </summary>
    [ImporterHeader(Name = "库位")]
    [Required(ErrorMessage = "库位不能为空")]
    public string StoreLocationCode { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [ImporterHeader(Name = "备注")]
    public string? Remark { get; set; }

}

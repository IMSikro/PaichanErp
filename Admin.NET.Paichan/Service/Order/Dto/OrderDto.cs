using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Paichan;

/// <summary>
/// 订单列表输出参数
/// </summary>
[ExcelImporter(IsLabelingError = true)]
[ExcelExporter(Name = "错误列表")]
public class OrderDto
{
    /// <summary>
    /// 订单编号
    /// </summary>
    [ImporterHeader(Name = "订单编号")]
    [ExporterHeader(DisplayName = "订单编号")]
    [Required(ErrorMessage = "订单编号不能为空")]
    public string OrderCode { get; set; }

    /// <summary>
    /// 下单日期
    /// </summary>
    [ImporterHeader(Name = "下单日期")]
    [ExporterHeader(DisplayName = "下单日期", Format = "yyyy/mm/DD")]
    [Required(ErrorMessage = "下单日期不能为空")]
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [ImporterHeader(Name = "交货日期")]
    [ExporterHeader(DisplayName = "交货日期", Format = "yyyy/mm/DD")]
    [Required(ErrorMessage = "交货日期不能为空")]
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [ImporterHeader(Name = "产品编号")]
    [ExporterHeader(DisplayName = "产品编号")]
    [Required(ErrorMessage = "产品编号不能为空")]
    public string ProduceCode { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    [ImporterHeader(Name = "批次号")]
    [ExporterHeader(DisplayName = "批次号")]
    [Required(ErrorMessage = "批次号不能为空")]
    public string BatchNumber { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [ImporterHeader(Name = "数量")]
    [ExporterHeader(DisplayName = "数量")]
    [Required(ErrorMessage = "数量不能为空")]
    public double Quantity { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [ImporterHeader(Name = "计量单位")]
    [ExporterHeader(DisplayName = "计量单位")]
    public string? pUnit { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    [ImporterHeader(Name = "客户")]
    [ExporterHeader(DisplayName = "客户")]
    [Required(ErrorMessage = "客户不能为空")]
    public string Customer { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    [ImporterHeader(Name = "设备编号")]
    [ExporterHeader(DisplayName = "设备编号")]
    public string? DeviceCode { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [ImporterHeader(Name = "备注")]
    [ExporterHeader(DisplayName = "备注")]
    public string? Remark { get; set; }

}
public class OrderExtenDto : OrderDto
{
    public string OrderDate2 => OrderDate.ToString("yyyy/MM/dd");

    public string DeliveryDate2 => DeliveryDate.ToString("yyyy/MM/dd");
}

public class OrderDtoTemp
{
    public List<OrderExtenDto> OrderDtos { get; set; }
}
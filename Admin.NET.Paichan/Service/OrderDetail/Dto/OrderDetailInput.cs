using Admin.NET.Core;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 订单排产基础输入参数
/// </summary>
public class OrderDetailBaseInput
{
    /// <summary>
    /// 订单批号
    /// </summary>
    public virtual long OrderId { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public virtual DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public virtual DateTime? EndDate { get; set; }

    /// <summary>
    /// 设备外键
    /// </summary>
    public virtual long DeviceId { get; set; }

    /// <summary>
    /// 设备异常时间
    /// </summary>
    public virtual double? DeviceErrorTime { get; set; }

    /// <summary>
    /// 设备未生产类型
    /// </summary>
    public virtual string? DeviceErrorType { get; set; }

    /// <summary>
    /// 设备未生产类型
    /// </summary>
    public virtual long? DeviceErrorTypeId { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public virtual string OperatorUsers { get; set; }

    /// <summary>
    /// 班次产量
    /// </summary>
    public virtual double? Qty { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    public virtual string? pUnit { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public virtual int? Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public virtual string? Remark { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public virtual string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public virtual string? UpdateUserName { get; set; }

}

/// <summary>
/// 订单排产分页查询输入参数
/// </summary>
public class OrderDetailInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 订单批号
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// 班次序号
    /// </summary>
    public string? OrderDetailCode { get; set; }

}

/// <summary>
/// 查询设备所有排产
/// </summary>
public class OrderDetailDeviceInput
{
    /// <summary>
    /// 设备Id
    /// </summary>
    public long? DeviceId { get; set; }
}

/// <summary>
/// 查询工艺所有排产
/// </summary>
public class OrderDetailDeviceTypeInput
{
    /// <summary>
    /// 设备类型(工艺)Id
    /// </summary>
    public long? DeviceTypeId { get; set; }
    /// <summary>
    /// 设备分组Id
    /// </summary>
    public long? GroupId { get; set; }
}
/// <summary>
/// 排产信息
/// </summary>
public class PaiOrderDetailInput : BasePageInput
{
    /// <summary>
    /// 设备Id
    /// </summary>
    public long? DeviceId { get; set; }

    /// <summary>
    /// 是否开工
    /// </summary>
    public bool? IsStart { get; set; }
    /// <summary>
    /// 是否完工
    /// </summary>
    public bool? IsEnd { get; set; }

}

/// <summary>
/// 订单排产增加输入参数
/// </summary>
public class AddOrderDetailInput : OrderDetailBaseInput
{
    /// <summary>
    /// 订单批号
    /// </summary>
    [Required(ErrorMessage = "订单批号不能为空")]
    public override long OrderId { get; set; }

    /// <summary>
    /// 设备外键
    /// </summary>
    [Required(ErrorMessage = "设备外键不能为空")]
    public override long DeviceId { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public override string OperatorUsers { get; set; }

}

/// <summary>
/// 订单排产删除输入参数
/// </summary>
public class DeleteOrderDetailInput : BaseIdInput
{
}

/// <summary>
/// 订单排产更新输入参数
/// </summary>
public class UpdateOrderDetailInput : OrderDetailBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

}

/// <summary>
/// 订单排产更新输入参数
/// </summary>
public class DoneOrderDetailInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 班次数量
    /// </summary>
    [Required(ErrorMessage = "班次数量不能为空")]
    public double? Qty { get; set; }

    /// <summary>
    /// 设备异常时间
    /// </summary>
    public virtual double? DeviceErrorTime { get; set; }


    /// <summary>
    /// 设备未生产类型
    /// </summary>
    public virtual long? DeviceErrorTypeId { get; set; }

}


/// <summary>
/// 设备排产更新排序
/// </summary>
public class SetOrderDetailSortInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序字段不能为空")]
    public int? Sort { get; set; }

}

/// <summary>
/// 订单排产主键查询输入参数
/// </summary>
public class QueryByIdOrderDetailInput : DeleteOrderDetailInput
{

}

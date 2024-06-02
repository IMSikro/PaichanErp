using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 设备列表基础输入参数
/// </summary>
public class DeviceBaseInput
{
    /// <summary>
    /// 工艺设备
    /// </summary>
    public virtual long DeviceTypeId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public virtual string DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public virtual string DeviceName { get; set; }

    /// <summary>
    /// 设备系数
    /// </summary>
    public virtual string? DeviceCoefficient { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string OperatorUsers { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

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
/// 设备列表分页查询输入参数
/// </summary>
public class DeviceInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public long? DeviceTypeId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string? DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// 分组Id
    /// </summary>
    public long? GroupId { get; set; }

}

/// <summary>
/// 设备列表增加输入参数
/// </summary>
public class AddDeviceInput : DeviceBaseInput
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [Required(ErrorMessage = "设备类型不能为空")]
    public override long DeviceTypeId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    [Required(ErrorMessage = "设备编号不能为空")]
    public override string DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    [Required(ErrorMessage = "设备名称不能为空")]
    public override string DeviceName { get; set; }

}

/// <summary>
/// 设备列表删除输入参数
/// </summary>
public class DeleteDeviceInput : BaseIdInput
{
}

/// <summary>
/// 设备列表更新输入参数
/// </summary>
public class UpdateDeviceInput : DeviceBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

}

/// <summary>
/// 设备列表主键查询输入参数
/// </summary>
public class QueryByIdDeviceInput : DeleteDeviceInput
{

}

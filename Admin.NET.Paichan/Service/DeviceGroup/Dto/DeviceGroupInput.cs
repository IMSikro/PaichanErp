using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 设备分组基础输入参数
/// </summary>
public class DeviceGroupBaseInput
{
    /// <summary>
    /// 分组编号
    /// </summary>
    public long? GroupCode { get; set; }

    /// <summary>
    /// 分组名
    /// </summary>
    public virtual string? GroupName { get; set; }

    /// <summary>
    /// 工艺设备
    /// </summary>
    public virtual string? DeviceTypeIds { get; set; }

    /// <summary>
    /// 设备
    /// </summary>
    public virtual string? DeviceIds { get; set; }

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
/// 设备分组分页查询输入参数
/// </summary>
public class DeviceGroupInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 分组编号
    /// </summary>
    public long? GroupCode { get; set; }

    /// <summary>
    /// 分组名
    /// </summary>
    public string? GroupName { get; set; }

}

/// <summary>
/// 设备分组增加输入参数
/// </summary>
public class AddDeviceGroupInput : DeviceGroupBaseInput
{
}

/// <summary>
/// 设备分组删除输入参数
/// </summary>
public class DeleteDeviceGroupInput : BaseIdInput
{
}

/// <summary>
/// 设备分组更新输入参数
/// </summary>
public class UpdateDeviceGroupInput : DeviceGroupBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

}

/// <summary>
/// 设备分组主键查询输入参数
/// </summary>
public class QueryByIdDeviceGroupInput : DeleteDeviceGroupInput
{

}

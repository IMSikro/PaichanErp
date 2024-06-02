namespace Admin.NET.Paichan;

/// <summary>
/// 设备列表输出参数
/// </summary>
public class DeviceOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 工艺设备
    /// </summary>
    public long DeviceTypeId { get; set; }

    /// <summary>
    /// 设备类型 描述
    /// </summary>
    public string DeviceTypeIdTypeName { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备系数
    /// </summary>
    public string? DeviceCoefficient { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string OperatorUsers { get; set; }

    /// <summary>
    /// 操作人员姓名
    /// </summary>
    public string OperatorUsersRealName { get; set; }

    /// <summary>
    /// 分组Id
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; set; }

}



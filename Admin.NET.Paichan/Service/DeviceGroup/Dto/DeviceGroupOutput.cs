namespace Admin.NET.Paichan;

/// <summary>
/// 设备分组输出参数
/// </summary>
public class DeviceGroupOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 分组编号
    /// </summary>
    public long? GroupCode { get; set; }
    
    /// <summary>
    /// 分组名
    /// </summary>
    public string? GroupName { get; set; }
    
    /// <summary>
    /// 工艺设备
    /// </summary>
    public string? DeviceTypeIds { get; set; } 
    
    /// <summary>
    /// 工艺设备 描述 
    /// </summary>
    public string? DeviceTypeIdsTypeName { get; set; } 
    
    /// <summary>
    /// 设备
    /// </summary>
    public string? DeviceIds { get; set; } 
    
    /// <summary>
    /// 设备 描述 
    /// </summary>
    public string? DeviceIdsDeviceCode { get; set; } 
    
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

    /// <summary>
    /// 工艺设备列表
    /// </summary>
    public List<DeviceTypeOutput> DeviceTypes { get; set; }

    /// <summary>
    /// 设备列表
    /// </summary>
    public List<DeviceOutput> Devices { get; set; }
}
 
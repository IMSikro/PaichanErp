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
    /// 设备类型
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
 


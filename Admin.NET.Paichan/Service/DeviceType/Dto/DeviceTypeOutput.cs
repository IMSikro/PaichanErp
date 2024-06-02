namespace Admin.NET.Paichan;

/// <summary>
/// 设备类型输出参数
/// </summary>
public class DeviceTypeOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public string TypeName { get; set; }

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

    /// <summary>
    /// 设备列表
    /// </summary>
    public List<DeviceOutput> Devices { get; set; }

    /// <summary>
    /// 未完成订单数
    /// </summary>
    public int UnOrderBatchNum { get; set; }

    /// <summary>
    /// 未完成数量
    /// </summary>
    public double UnOrderNumber { get; set; }

    /// <summary>
    /// 已排产订单数
    /// </summary>
    public int OrderBatchNum { get; set; }

    /// <summary>
    /// 已排产数量
    /// </summary>
    public double OrderNumber { get; set; }

    /// <summary>
    /// 已完工订单数
    /// </summary>
    public int EndOrderBatchNum { get; set; }

    /// <summary>
    /// 已完工数量
    /// </summary>
    public double EndOrderNumber { get; set; }

}



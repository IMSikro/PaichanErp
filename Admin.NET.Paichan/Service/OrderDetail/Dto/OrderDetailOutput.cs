namespace Admin.NET.Paichan;

/// <summary>
/// 订单排产输出参数
/// </summary>
public class OrderDetailOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 订单批号
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 订单批号 描述
    /// </summary>
    public string OrderIdBatchNumber { get; set; }

    /// <summary>
    /// 批次序号
    /// </summary>
    public string OrderDetailCode { get; set; }

    /// <summary>
    /// 班次序号
    /// </summary>
    public int? SN { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// 设备类型外键
    /// (排产工艺识别)
    /// </summary>
    public long? DeviceTypeId { get; set; }

    /// <summary>
    /// 设备外键
    /// </summary>
    public long DeviceId { get; set; }

    /// <summary>
    /// 设备异常时间
    /// </summary>
    public double? DeviceErrorTime { get; set; }

    /// <summary>
    /// 设备未生产类型
    /// </summary>
    public string? DeviceErrorType { get; set; }

    /// <summary>
    /// 设备未生产类型
    /// </summary>
    public long? DeviceErrorTypeId { get; set; }

    /// <summary>
    /// 设备外键 描述
    /// </summary>
    public string DeviceIdDeviceCode { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string OperatorUsers { get; set; }

    /// <summary>
    /// 是否完工
    /// </summary>
    public bool IsEnd { get; set; }

    /// <summary>
    /// 操作人员 描述
    /// </summary>
    public string OperatorUsersRealName { get; set; }

    /// <summary>
    /// 班次产量
    /// </summary>
    public double? Qty { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    public string? pUnit { get; set; }

    /// <summary>
    /// 产品选择
    /// </summary>
    public long? ProduceId { get; set; }
    /// <summary>
    /// 产品编号
    /// </summary>
    public string ProduceIdProduceName { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    public string ProduceCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProduceName { get; set; }

    /// <summary>
    /// 产品RGB颜色
    /// </summary>
    public string? ColorRgb { get; set; }

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


/// <summary>
/// 设备订单排产输出参数 - 二级目录
/// </summary>
public class OrderDetailByDeviceOutput
{
    /// <summary>
    /// 设备Id
    /// </summary>
    public long DeviceId { get; set; }

    /// <summary>
    /// 设备类型Id
    /// </summary>
    public long DeviceTypeId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string OperatorUsers { get; set; }

    /// <summary>
    /// 操作人员姓名
    /// </summary>
    public string OperatorUsersRealName { get; set; }

    /// <summary>
    /// 排产列表
    /// </summary>
    public List<OrderDetailOutput> OrderDetails { get; set; }
}

/// <summary>
/// 工艺订单排产输出参数 - 三级目录
/// </summary>
public class OrderDetailByDeviceTypeOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long DeviceTypeId { get; set; }

    /// <summary>
    /// 设备类型(工艺)
    /// </summary>
    public string DeviceTypeName { get; set; }

    /// <summary>
    /// 设备列表
    /// </summary>
    public List<OrderDetailByDeviceOutput> Devices { get; set; }
}

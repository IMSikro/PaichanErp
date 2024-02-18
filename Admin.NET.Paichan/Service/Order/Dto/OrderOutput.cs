namespace Admin.NET.Paichan;

/// <summary>
/// 订单列表输出参数
/// </summary>
public class OrderOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 订单编号
    /// </summary>
    public string OrderCode { get; set; }
    
    /// <summary>
    /// 下单日期
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    /// <summary>
    /// 交货日期
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    
    /// <summary>
    /// 实际开工时间
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// 实际完成时间
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// 产品选择
    /// </summary>
    public long ProduceId { get; set; } 
    
    /// <summary>
    /// 产品选择 描述
    /// </summary>
    public string ProduceIdProduceName { get; set; } 
    
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProduceName { get; set; }
    
    /// <summary>
    /// 批次号
    /// </summary>
    public string BatchNumber { get; set; }
    
    /// <summary>
    /// 批次总量
    /// </summary>
    public double Quantity { get; set; }
    
    /// <summary>
    /// 计量单位
    /// </summary>
    public string pUnit { get; set; }
    
    /// <summary>
    /// 客户
    /// </summary>
    public string Customer { get; set; }
    
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
    /// 订单排产列表
    /// </summary>
    public List<OrderDetailOutput> OrderDetails { get; set; }
    
    }
 


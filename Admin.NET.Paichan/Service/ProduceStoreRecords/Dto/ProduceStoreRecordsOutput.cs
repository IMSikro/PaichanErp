namespace Admin.NET.Paichan;

/// <summary>
/// 生产入库单输出参数
/// </summary>
public class ProduceStoreRecordsOutput
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
    /// 订单批号
    /// </summary>
    public string BatchNumber { get; set; }
    
    /// <summary>
    /// 批次总量
    /// </summary>
    public double Quantity { get; set; }
    
    /// <summary>
    /// 生产总量
    /// </summary>
    public double RealQuantity { get; set; }
    
    /// <summary>
    /// 批数
    /// </summary>
    public int BatchCount { get; set; }
    
    /// <summary>
    /// 产品
    /// </summary>
    public long ProduceId { get; set; } 
    
    /// <summary>
    /// 产品 描述
    /// </summary>
    public string ProduceIdOrderCode { get; set; } 
    
    /// <summary>
    /// 产品编号
    /// </summary>
    public string? ProduceCode { get; set; }
    
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProduceName { get; set; }
    
    /// <summary>
    /// 仓库
    /// </summary>
    public long StoreId { get; set; }
    
    /// <summary>
    /// 仓库编号
    /// </summary>
    public string StoreCode { get; set; }
    
    /// <summary>
    /// 仓库名称
    /// </summary>
    public string StoreName { get; set; }
    
    /// <summary>
    /// 库位
    /// </summary>
    public string? StoreLocation { get; set; }
    
    /// <summary>
    /// 入库产值
    /// </summary>
    public double? TotalPrice { get; set; }
    
    /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime? InStoreDate { get; set; }
    
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
 


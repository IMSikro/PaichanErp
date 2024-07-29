namespace Admin.NET.Paichan;

/// <summary>
/// 库存盘点输出参数
/// </summary>
public class StockTakeOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 盘点单号
    /// </summary>
    public string? StockTakeCode { get; set; }
    
    /// <summary>
    /// 盘点日期
    /// </summary>
    public DateTime? StockTakeDate { get; set; }
    
    /// <summary>
    /// 盘点数量
    /// </summary>
    public double? StockTakeNumber { get; set; }
    
    /// <summary>
    /// 盈亏数量
    /// </summary>
    public double? StockTakeDiffNumber { get; set; }
    
    /// <summary>
    /// 产品
    /// </summary>
    public long ProduceId { get; set; } 
    
    /// <summary>
    /// 产品 描述
    /// </summary>
    public string ProduceIdProduceCode { get; set; } 
    
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
    /// 仓库 描述
    /// </summary>
    public string StoreIdStoreCode { get; set; } 
    
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
 


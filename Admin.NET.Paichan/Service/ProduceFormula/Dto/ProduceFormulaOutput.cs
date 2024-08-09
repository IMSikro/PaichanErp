namespace Admin.NET.Paichan;

/// <summary>
/// 产品配方输出参数
/// </summary>
public class ProduceFormulaOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 配方编号
    /// </summary>
    public string ProduceFormulaCode { get; set; }
    
    /// <summary>
    /// 产品外键
    /// </summary>
    public long ProduceId { get; set; }
    
    /// <summary>
    /// 产品外键 描述
    /// </summary>
    public string ProduceIdProduceCode { get; set; } 
    
    /// <summary>
    /// 产品编号
    /// </summary>
    public string ProduceCode { get; set; } 
    
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProduceName { get; set; } 
    
    /// <summary>
    /// 配方名称
    /// </summary>
    public string ProduceFormulaName { get; set; }
    
    /// <summary>
    /// 配方版本
    /// </summary>
    public string FormulaVersion { get; set; }
    
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    
    /// <summary>
    /// 配方成本
    /// </summary>
    public double FormulaCosts { get; set; }
    
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
 

